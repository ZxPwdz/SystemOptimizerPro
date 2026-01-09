using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemoryService _memoryService;
    private readonly IProcessService _processService;
    private readonly StandbyListService _standbyListService;
    private readonly DnsCacheService _dnsCacheService;
    private readonly RecentFilesService _recentFilesService;
    private readonly ISettingsService _settingsService;
    private readonly DispatcherTimer _monitorTimer;

    [ObservableProperty]
    private object? _currentView;

    [ObservableProperty]
    private string _statusMessage = "Ready";

    [ObservableProperty]
    private int _ramUsagePercent;

    [ObservableProperty]
    private int _cpuUsagePercent;

    [ObservableProperty]
    private Brush _ramUsageColor = Brushes.Green;

    [ObservableProperty]
    private Brush _cpuUsageColor = Brushes.Green;

    [ObservableProperty]
    private string _monitoringStatus = "Monitoring";

    [ObservableProperty]
    private Brush _monitoringStatusColor = Brushes.Green;

    [ObservableProperty]
    private bool _isOptimizing;

    public MainViewModel(
        IServiceProvider serviceProvider,
        IMemoryService memoryService,
        IProcessService processService,
        StandbyListService standbyListService,
        DnsCacheService dnsCacheService,
        RecentFilesService recentFilesService,
        ISettingsService settingsService)
    {
        _serviceProvider = serviceProvider;
        _memoryService = memoryService;
        _processService = processService;
        _standbyListService = standbyListService;
        _dnsCacheService = dnsCacheService;
        _recentFilesService = recentFilesService;
        _settingsService = settingsService;

        // Initialize with Dashboard
        CurrentView = _serviceProvider.GetRequiredService<DashboardViewModel>();

        // Setup monitoring timer
        _monitorTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _monitorTimer.Tick += OnMonitorTick;
        _monitorTimer.Start();
    }

    private async void OnMonitorTick(object? sender, EventArgs e)
    {
        await UpdateSystemMetricsAsync();
    }

    private async Task UpdateSystemMetricsAsync()
    {
        try
        {
            var memInfo = await _memoryService.GetMemoryInfoAsync();
            RamUsagePercent = (int)memInfo.UsagePercent;
            RamUsageColor = GetUsageColor(RamUsagePercent);

            var cpuUsage = await _processService.GetCpuUsageAsync();
            CpuUsagePercent = (int)cpuUsage;
            CpuUsageColor = GetUsageColor(CpuUsagePercent);
        }
        catch
        {
            // Silently handle errors in monitoring
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
    private void Navigate(string? destination)
    {
        if (string.IsNullOrEmpty(destination)) return;

        CurrentView = destination switch
        {
            "Dashboard" => _serviceProvider.GetRequiredService<DashboardViewModel>(),
            "Memory" => _serviceProvider.GetRequiredService<MemoryCleanerViewModel>(),
            "Processes" => _serviceProvider.GetRequiredService<ProcessesViewModel>(),
            "Cleaning" => _serviceProvider.GetRequiredService<CleaningToolsViewModel>(),
            "Settings" => _serviceProvider.GetRequiredService<SettingsViewModel>(),
            _ => CurrentView
        };
    }

    [RelayCommand]
    private async Task QuickOptimizeAsync()
    {
        if (IsOptimizing) return;

        IsOptimizing = true;
        StatusMessage = "Optimizing...";
        MonitoringStatus = "Cleaning";
        MonitoringStatusColor = (Brush)Application.Current.Resources["AccentWarningBrush"];

        try
        {
            // Clear standby list
            var standbyResult = await _standbyListService.PurgeNowAsync();

            // Flush DNS
            await _dnsCacheService.FlushDnsCacheAsync();

            // Clear recent files
            await _recentFilesService.ClearRecentFilesAsync();

            // Update settings
            var settings = _settingsService.GetSettings();
            settings.LastMemoryClean = DateTime.Now;
            settings.LastDnsFlush = DateTime.Now;
            settings.LastRecentFilesClear = DateTime.Now;
            _settingsService.SaveSettings(settings);

            // Update status
            StatusMessage = $"Optimized - Freed {standbyResult.MemoryFreedFormatted} at {DateTime.Now:HH:mm}";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsOptimizing = false;
            MonitoringStatus = "Monitoring";
            MonitoringStatusColor = (Brush)Application.Current.Resources["AccentSecondaryBrush"];
        }
    }
}
