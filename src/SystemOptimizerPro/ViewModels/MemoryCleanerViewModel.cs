using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class MemoryCleanerViewModel : BaseViewModel
{
    private readonly IMemoryService _memoryService;
    private readonly StandbyListService _standbyListService;
    private readonly ISettingsService _settingsService;
    private readonly DispatcherTimer _updateTimer;

    [ObservableProperty]
    private double _totalMemoryGB;

    [ObservableProperty]
    private double _usedMemoryGB;

    [ObservableProperty]
    private double _standbyMemoryGB;

    [ObservableProperty]
    private double _freeMemoryGB;

    [ObservableProperty]
    private int _usagePercent;

    [ObservableProperty]
    private Brush _usageColor = Brushes.Green;

    [ObservableProperty]
    private bool _autoPurgeEnabled;

    [ObservableProperty]
    private int _standbyThresholdMB = 1024;

    [ObservableProperty]
    private int _freeMemoryThresholdMB = 1024;

    [ObservableProperty]
    private int _pollingRateMs = 1000;

    [ObservableProperty]
    private int _purgeCount;

    [ObservableProperty]
    private string _lastPurgeResult = "No purge performed yet";

    [ObservableProperty]
    private bool _isPurging;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public MemoryCleanerViewModel(
        IMemoryService memoryService,
        StandbyListService standbyListService,
        ISettingsService settingsService)
    {
        _memoryService = memoryService;
        _standbyListService = standbyListService;
        _settingsService = settingsService;

        // Load settings
        LoadSettings();

        // Subscribe to events
        _standbyListService.StandbyListPurged += OnStandbyListPurged;
        _standbyListService.MemoryUpdated += OnMemoryUpdated;

        // Setup update timer
        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _updateTimer.Tick += async (s, e) => await UpdateMemoryInfoAsync();
        _updateTimer.Start();

        // Initial update
        _ = UpdateMemoryInfoAsync();
    }

    private void LoadSettings()
    {
        var settings = _settingsService.GetSettings();
        AutoPurgeEnabled = settings.AutoPurgeEnabled;
        StandbyThresholdMB = settings.StandbyThresholdMB;
        FreeMemoryThresholdMB = settings.FreeMemoryThresholdMB;
        PollingRateMs = settings.PollingRateMs;
    }

    partial void OnAutoPurgeEnabledChanged(bool value)
    {
        var settings = _settingsService.GetSettings();
        settings.AutoPurgeEnabled = value;
        _settingsService.SaveSettings(settings);

        if (value)
        {
            _standbyListService.StartMonitoring();
            StatusMessage = "Auto-purge enabled";
        }
        else
        {
            _standbyListService.StopMonitoring();
            StatusMessage = "Auto-purge disabled";
        }
    }

    partial void OnStandbyThresholdMBChanged(int value)
    {
        var settings = _settingsService.GetSettings();
        settings.StandbyThresholdMB = value;
        _settingsService.SaveSettings(settings);
    }

    partial void OnFreeMemoryThresholdMBChanged(int value)
    {
        var settings = _settingsService.GetSettings();
        settings.FreeMemoryThresholdMB = value;
        _settingsService.SaveSettings(settings);
    }

    private async Task UpdateMemoryInfoAsync()
    {
        try
        {
            var memInfo = await _memoryService.GetMemoryInfoAsync();
            TotalMemoryGB = memInfo.TotalGB;
            UsedMemoryGB = memInfo.UsedGB;
            StandbyMemoryGB = memInfo.StandbyGB;
            FreeMemoryGB = memInfo.AvailableGB;
            UsagePercent = (int)memInfo.UsagePercent;
            UsageColor = GetUsageColor(UsagePercent);
            PurgeCount = _standbyListService.PurgeCount;
        }
        catch
        {
            // Handle error
        }
    }

    private void OnStandbyListPurged(object? sender, StandbyListPurgedEventArgs e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            LastPurgeResult = e.Success
                ? $"Freed {e.MemoryFreedFormatted} at {e.Timestamp:HH:mm:ss}"
                : "Purge failed";
            PurgeCount = _standbyListService.PurgeCount;
        });
    }

    private void OnMemoryUpdated(object? sender, MemoryInfo e)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            TotalMemoryGB = e.TotalGB;
            UsedMemoryGB = e.UsedGB;
            StandbyMemoryGB = e.StandbyGB;
            FreeMemoryGB = e.AvailableGB;
            UsagePercent = (int)e.UsagePercent;
            UsageColor = GetUsageColor(UsagePercent);
        });
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
    private async Task PurgeStandbyListAsync()
    {
        if (IsPurging) return;

        IsPurging = true;
        StatusMessage = "Purging standby list...";

        try
        {
            var result = await _standbyListService.PurgeNowAsync();
            StatusMessage = result.Success
                ? $"Freed {result.MemoryFreedFormatted}"
                : "Purge failed";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsPurging = false;
        }
    }

    [RelayCommand]
    private async Task EmptyWorkingSetsAsync()
    {
        if (IsPurging) return;

        IsPurging = true;
        StatusMessage = "Emptying working sets...";

        try
        {
            var success = await _memoryService.EmptyWorkingSetsAsync();
            StatusMessage = success ? "Working sets emptied" : "Failed to empty working sets";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsPurging = false;
        }
    }

    public override void Cleanup()
    {
        _updateTimer.Stop();
        _standbyListService.StandbyListPurged -= OnStandbyListPurged;
        _standbyListService.MemoryUpdated -= OnMemoryUpdated;
    }
}
