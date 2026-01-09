using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IMemoryService _memoryService;
    private readonly IProcessService _processService;
    private readonly StandbyListService _standbyListService;
    private readonly DnsCacheService _dnsCacheService;
    private readonly RecentFilesService _recentFilesService;
    private readonly IRegistryService _registryService;
    private readonly ISettingsService _settingsService;
    private readonly DispatcherTimer _updateTimer;

    private readonly ObservableCollection<ObservableValue> _ramHistory = new();
    private readonly ObservableCollection<ObservableValue> _cpuHistory = new();
    private const int MaxHistoryPoints = 60;

    [ObservableProperty]
    private int _ramUsagePercent;

    [ObservableProperty]
    private double _ramUsedGB;

    [ObservableProperty]
    private double _ramTotalGB;

    [ObservableProperty]
    private Brush _ramProgressColor = Brushes.Green;

    [ObservableProperty]
    private int _cpuUsagePercent;

    [ObservableProperty]
    private string _cpuSpeed = "0 GHz";

    [ObservableProperty]
    private Brush _cpuProgressColor = Brushes.Green;

    [ObservableProperty]
    private int _processCount;

    [ObservableProperty]
    private string _processChange = "from last hour";

    [ObservableProperty]
    private double _inUseGB;

    [ObservableProperty]
    private double _standbyGB;

    [ObservableProperty]
    private double _freeGB;

    [ObservableProperty]
    private string _inUseWidth = "*";

    [ObservableProperty]
    private string _standbyWidth = "*";

    [ObservableProperty]
    private string _freeWidth = "*";

    [ObservableProperty]
    private ObservableCollection<HealthItem> _healthItems = new();

    [ObservableProperty]
    private string _lastOptimizedTime = "Never";

    [ObservableProperty]
    private bool _isClearing;

    public ISeries[] RamSeries { get; }
    public ISeries[] CpuSeries { get; }

    public DashboardViewModel(
        IMemoryService memoryService,
        IProcessService processService,
        StandbyListService standbyListService,
        DnsCacheService dnsCacheService,
        RecentFilesService recentFilesService,
        IRegistryService registryService,
        ISettingsService settingsService)
    {
        _memoryService = memoryService;
        _processService = processService;
        _standbyListService = standbyListService;
        _dnsCacheService = dnsCacheService;
        _recentFilesService = recentFilesService;
        _registryService = registryService;
        _settingsService = settingsService;

        // Initialize chart series
        RamSeries = new ISeries[]
        {
            new LineSeries<ObservableValue>
            {
                Values = _ramHistory,
                Fill = new SolidColorPaint(SKColor.Parse("#201A73E8")),
                Stroke = new SolidColorPaint(SKColor.Parse("#1A73E8")) { StrokeThickness = 2 },
                GeometrySize = 0,
                LineSmoothness = 0.5
            }
        };

        CpuSeries = new ISeries[]
        {
            new LineSeries<ObservableValue>
            {
                Values = _cpuHistory,
                Fill = new SolidColorPaint(SKColor.Parse("#20A142F4")),
                Stroke = new SolidColorPaint(SKColor.Parse("#A142F4")) { StrokeThickness = 2 },
                GeometrySize = 0,
                LineSmoothness = 0.5
            }
        };

        // Setup update timer
        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _updateTimer.Tick += async (s, e) => await UpdateDashboardAsync();
        _updateTimer.Start();

        // Initial load
        _ = InitializeAsync();
    }

    public override async Task InitializeAsync()
    {
        await UpdateDashboardAsync();
        await UpdateHealthItemsAsync();
        UpdateLastOptimizedTime();
    }

    private async Task UpdateDashboardAsync()
    {
        try
        {
            // Update memory info
            var memInfo = await _memoryService.GetMemoryInfoAsync();
            RamUsagePercent = (int)memInfo.UsagePercent;
            RamUsedGB = memInfo.UsedGB;
            RamTotalGB = memInfo.TotalGB;
            RamProgressColor = GetUsageColor(RamUsagePercent);

            InUseGB = memInfo.UsedGB - memInfo.StandbyGB;
            StandbyGB = memInfo.StandbyGB;
            FreeGB = memInfo.AvailableGB;

            // Update memory breakdown widths
            double total = InUseGB + StandbyGB + FreeGB;
            if (total > 0)
            {
                InUseWidth = $"{InUseGB / total:F2}*";
                StandbyWidth = $"{StandbyGB / total:F2}*";
                FreeWidth = $"{FreeGB / total:F2}*";
            }

            // Update CPU info
            var cpuUsage = await _processService.GetCpuUsageAsync();
            CpuUsagePercent = (int)cpuUsage;
            CpuProgressColor = GetUsageColor(CpuUsagePercent);

            // Update process count
            ProcessCount = (int)memInfo.ProcessCount;

            // Update history for charts
            _ramHistory.Add(new ObservableValue(RamUsagePercent));
            _cpuHistory.Add(new ObservableValue(CpuUsagePercent));

            if (_ramHistory.Count > MaxHistoryPoints)
                _ramHistory.RemoveAt(0);
            if (_cpuHistory.Count > MaxHistoryPoints)
                _cpuHistory.RemoveAt(0);
        }
        catch
        {
            // Handle errors silently
        }
    }

    private async Task UpdateHealthItemsAsync()
    {
        try
        {
            var items = new ObservableCollection<HealthItem>();

            // Memory health
            var memInfo = await _memoryService.GetMemoryInfoAsync();
            items.Add(new HealthItem
            {
                Name = "Memory",
                Value = memInfo.UsagePercent < 80 ? "Good" : "High Usage",
                Status = memInfo.UsagePercent < 80 ? HealthStatus.Good : HealthStatus.Warning
            });

            // DNS cache
            var dnsCount = await _dnsCacheService.GetCacheEntryCountAsync();
            items.Add(new HealthItem
            {
                Name = "DNS Cache",
                Value = $"{dnsCount:N0} entries",
                Status = dnsCount < 1000 ? HealthStatus.Good : HealthStatus.Warning
            });

            // Recent files
            var recentInfo = await _recentFilesService.GetRecentFilesInfoAsync();
            items.Add(new HealthItem
            {
                Name = "Recent Files",
                Value = $"{recentInfo.TotalCount} items",
                Status = recentInfo.TotalCount < 200 ? HealthStatus.Good : HealthStatus.Warning
            });

            // Registry - just show status based on last scan
            items.Add(new HealthItem
            {
                Name = "Registry",
                Value = "Scan to check",
                Status = HealthStatus.Unknown
            });

            HealthItems = items;
        }
        catch
        {
            // Handle errors
        }
    }

    private void UpdateLastOptimizedTime()
    {
        var settings = _settingsService.GetSettings();
        if (settings.LastMemoryClean.HasValue)
        {
            var diff = DateTime.Now - settings.LastMemoryClean.Value;
            LastOptimizedTime = diff.TotalMinutes < 60
                ? $"{(int)diff.TotalMinutes} minutes ago"
                : diff.TotalHours < 24
                    ? $"{(int)diff.TotalHours} hours ago"
                    : $"{(int)diff.TotalDays} days ago";
        }
    }

    private static Brush GetUsageColor(int percent)
    {
        return percent switch
        {
            < 50 => (Brush)Application.Current.Resources["AccentSecondaryBrush"],
            < 80 => (Brush)Application.Current.Resources["AccentWarningBrush"],
            _ => (Brush)Application.Current.Resources["AccentDangerBrush"]
        };
    }

    [RelayCommand]
    private async Task ClearStandbyAsync()
    {
        if (IsClearing) return;
        IsClearing = true;

        try
        {
            await _standbyListService.PurgeNowAsync();
            await UpdateDashboardAsync();
        }
        finally
        {
            IsClearing = false;
        }
    }

    [RelayCommand]
    private async Task FlushDnsAsync()
    {
        if (IsClearing) return;
        IsClearing = true;

        try
        {
            await _dnsCacheService.FlushDnsCacheAsync();
            await UpdateHealthItemsAsync();
        }
        finally
        {
            IsClearing = false;
        }
    }

    [RelayCommand]
    private async Task ClearRecentAsync()
    {
        if (IsClearing) return;
        IsClearing = true;

        try
        {
            await _recentFilesService.ClearRecentFilesAsync();
            await UpdateHealthItemsAsync();
        }
        finally
        {
            IsClearing = false;
        }
    }

    [RelayCommand]
    private async Task CleanRegistryAsync()
    {
        if (IsClearing) return;
        IsClearing = true;

        try
        {
            var issues = await _registryService.ScanForIssuesAsync();
            if (issues.Count > 0)
            {
                await _registryService.CreateBackupAsync();
                await _registryService.CleanIssuesAsync(issues);
            }
            await UpdateHealthItemsAsync();
        }
        finally
        {
            IsClearing = false;
        }
    }

    public override void Cleanup()
    {
        _updateTimer.Stop();
    }
}
