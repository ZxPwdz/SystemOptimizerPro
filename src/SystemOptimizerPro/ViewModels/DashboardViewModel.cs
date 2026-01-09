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
    private readonly ActionLogService _actionLogService;
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

    public ObservableCollection<ActionLogEntry> ActionLog => _actionLogService.ActionLog;

    public ISeries[] RamSeries { get; }
    public ISeries[] CpuSeries { get; }

    public DashboardViewModel(
        IMemoryService memoryService,
        IProcessService processService,
        StandbyListService standbyListService,
        DnsCacheService dnsCacheService,
        RecentFilesService recentFilesService,
        IRegistryService registryService,
        ISettingsService settingsService,
        ActionLogService actionLogService)
    {
        _memoryService = memoryService;
        _processService = processService;
        _standbyListService = standbyListService;
        _dnsCacheService = dnsCacheService;
        _recentFilesService = recentFilesService;
        _registryService = registryService;
        _settingsService = settingsService;
        _actionLogService = actionLogService;

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
            var memStatus = memInfo.UsagePercent < 60 ? HealthStatus.Good
                          : memInfo.UsagePercent < 85 ? HealthStatus.Warning
                          : HealthStatus.Critical;
            items.Add(new HealthItem
            {
                Name = "Memory",
                Value = memStatus == HealthStatus.Good ? "Good" : memStatus == HealthStatus.Warning ? "Moderate" : "High",
                NumericValue = (int)memInfo.UsagePercent,
                Status = memStatus
            });

            // DNS cache - 0 is best (green), high is warning/red
            var dnsCount = await _dnsCacheService.GetCacheEntryCountAsync();
            var dnsStatus = dnsCount == 0 ? HealthStatus.Good
                          : dnsCount < 500 ? HealthStatus.Good
                          : dnsCount < 2000 ? HealthStatus.Warning
                          : HealthStatus.Critical;
            items.Add(new HealthItem
            {
                Name = "DNS Cache",
                Value = $"{dnsCount:N0} entries",
                NumericValue = dnsCount,
                Status = dnsStatus
            });

            // Recent files - 0 is best (green), high is warning/red
            var recentInfo = await _recentFilesService.GetRecentFilesInfoAsync();
            var recentStatus = recentInfo.TotalCount == 0 ? HealthStatus.Good
                             : recentInfo.TotalCount < 50 ? HealthStatus.Good
                             : recentInfo.TotalCount < 200 ? HealthStatus.Warning
                             : HealthStatus.Critical;
            items.Add(new HealthItem
            {
                Name = "Recent Files",
                Value = $"{recentInfo.TotalCount} items",
                NumericValue = recentInfo.TotalCount,
                Status = recentStatus
            });

            // Registry - show last scan result or prompt to scan
            var settings = _settingsService.GetSettings();
            var regStatus = HealthStatus.Unknown;
            var regValue = "Scan to check";
            if (settings.LastRegistryClean.HasValue)
            {
                var diff = DateTime.Now - settings.LastRegistryClean.Value;
                if (diff.TotalHours < 24)
                {
                    regStatus = HealthStatus.Good;
                    regValue = "Clean";
                }
                else if (diff.TotalDays < 7)
                {
                    regStatus = HealthStatus.Warning;
                    regValue = "Scan recommended";
                }
            }
            items.Add(new HealthItem
            {
                Name = "Registry",
                Value = regValue,
                NumericValue = 0,
                Status = regStatus
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

    private void AddLogEntry(string action, string details, bool success)
    {
        _actionLogService.AddEntry(action, details, success);
    }

    [RelayCommand]
    private async Task ClearStandbyAsync()
    {
        if (IsClearing) return;
        IsClearing = true;

        try
        {
            var beforeMem = await _memoryService.GetMemoryInfoAsync();
            await _standbyListService.PurgeNowAsync();
            var afterMem = await _memoryService.GetMemoryInfoAsync();

            var freedMB = (beforeMem.StandbyGB - afterMem.StandbyGB) * 1024;
            AddLogEntry("Clear Standby", $"Freed {freedMB:F0} MB", true);

            var settings = _settingsService.GetSettings();
            settings.LastMemoryClean = DateTime.Now;
            _settingsService.SaveSettings(settings);

            await UpdateDashboardAsync();
            UpdateLastOptimizedTime();
        }
        catch (Exception ex)
        {
            AddLogEntry("Clear Standby", $"Failed: {ex.Message}", false);
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
            var beforeCount = await _dnsCacheService.GetCacheEntryCountAsync();
            var result = await _dnsCacheService.FlushDnsCacheAsync();

            AddLogEntry("Flush DNS", $"Cleared {beforeCount} entries", result.Success);

            var settings = _settingsService.GetSettings();
            settings.LastDnsFlush = DateTime.Now;
            _settingsService.SaveSettings(settings);

            await UpdateHealthItemsAsync();
        }
        catch (Exception ex)
        {
            AddLogEntry("Flush DNS", $"Failed: {ex.Message}", false);
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
            var beforeInfo = await _recentFilesService.GetRecentFilesInfoAsync();
            var result = await _recentFilesService.ClearRecentFilesAsync();

            AddLogEntry("Clear Recent", $"Removed {beforeInfo.TotalCount} items", result.Success);

            var settings = _settingsService.GetSettings();
            settings.LastRecentFilesClear = DateTime.Now;
            _settingsService.SaveSettings(settings);

            await UpdateHealthItemsAsync();
        }
        catch (Exception ex)
        {
            AddLogEntry("Clear Recent", $"Failed: {ex.Message}", false);
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
                var result = await _registryService.CleanIssuesAsync(issues);
                AddLogEntry("Clean Registry", $"Fixed {result.ItemsProcessed} issues", result.Success);

                var settings = _settingsService.GetSettings();
                settings.LastRegistryClean = DateTime.Now;
                _settingsService.SaveSettings(settings);
            }
            else
            {
                AddLogEntry("Clean Registry", "No issues found", true);
            }
            await UpdateHealthItemsAsync();
        }
        catch (Exception ex)
        {
            AddLogEntry("Clean Registry", $"Failed: {ex.Message}", false);
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
