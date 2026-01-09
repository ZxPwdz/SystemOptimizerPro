using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;
    private readonly StartupService _startupService;

    [ObservableProperty]
    private bool _startWithWindows;

    [ObservableProperty]
    private bool _startMinimized;

    [ObservableProperty]
    private bool _minimizeToTray;

    [ObservableProperty]
    private bool _showNotifications;

    [ObservableProperty]
    private bool _confirmBeforeClean;

    [ObservableProperty]
    private bool _createRegistryBackup;

    [ObservableProperty]
    private bool _autoPurgeEnabled;

    [ObservableProperty]
    private int _standbyThresholdMB;

    [ObservableProperty]
    private int _freeMemoryThresholdMB;

    [ObservableProperty]
    private int _pollingRateMs;

    [ObservableProperty]
    private int _monitoringRefreshRateMs;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private string _appVersion = "1.0.0";

    public SettingsViewModel(ISettingsService settingsService, StartupService startupService)
    {
        _settingsService = settingsService;
        _startupService = startupService;

        LoadSettings();
    }

    private void LoadSettings()
    {
        var settings = _settingsService.GetSettings();

        StartWithWindows = _startupService.IsStartupEnabled();
        StartMinimized = settings.StartMinimized;
        MinimizeToTray = settings.MinimizeToTray;
        ShowNotifications = settings.ShowNotifications;
        ConfirmBeforeClean = settings.ConfirmBeforeClean;
        CreateRegistryBackup = settings.CreateRegistryBackup;
        AutoPurgeEnabled = settings.AutoPurgeEnabled;
        StandbyThresholdMB = settings.StandbyThresholdMB;
        FreeMemoryThresholdMB = settings.FreeMemoryThresholdMB;
        PollingRateMs = settings.PollingRateMs;
        MonitoringRefreshRateMs = settings.MonitoringRefreshRateMs;
    }

    partial void OnStartWithWindowsChanged(bool value)
    {
        _startupService.SetStartup(value);
        SaveSetting(nameof(StartWithWindows), value);
    }

    partial void OnStartMinimizedChanged(bool value)
    {
        SaveSetting(nameof(StartMinimized), value);
    }

    partial void OnMinimizeToTrayChanged(bool value)
    {
        SaveSetting(nameof(MinimizeToTray), value);
    }

    partial void OnShowNotificationsChanged(bool value)
    {
        SaveSetting(nameof(ShowNotifications), value);
    }

    partial void OnConfirmBeforeCleanChanged(bool value)
    {
        SaveSetting(nameof(ConfirmBeforeClean), value);
    }

    partial void OnCreateRegistryBackupChanged(bool value)
    {
        SaveSetting(nameof(CreateRegistryBackup), value);
    }

    partial void OnAutoPurgeEnabledChanged(bool value)
    {
        SaveSetting(nameof(AutoPurgeEnabled), value);
    }

    partial void OnStandbyThresholdMBChanged(int value)
    {
        SaveSetting(nameof(StandbyThresholdMB), value);
    }

    partial void OnFreeMemoryThresholdMBChanged(int value)
    {
        SaveSetting(nameof(FreeMemoryThresholdMB), value);
    }

    partial void OnPollingRateMsChanged(int value)
    {
        SaveSetting(nameof(PollingRateMs), value);
    }

    partial void OnMonitoringRefreshRateMsChanged(int value)
    {
        SaveSetting(nameof(MonitoringRefreshRateMs), value);
    }

    private void SaveSetting<T>(string name, T value)
    {
        var settings = _settingsService.GetSettings();
        var property = typeof(Models.AppSettings).GetProperty(name);
        if (property != null && property.CanWrite)
        {
            property.SetValue(settings, value);
            _settingsService.SaveSettings(settings);
        }
    }

    [RelayCommand]
    private void ResetToDefaults()
    {
        var defaultSettings = new Models.AppSettings();
        _settingsService.SaveSettings(defaultSettings);
        LoadSettings();
        StatusMessage = "Settings reset to defaults";
    }

    [RelayCommand]
    private void OpenDataFolder()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SystemOptimizerPro");

        if (Directory.Exists(folder))
        {
            System.Diagnostics.Process.Start("explorer.exe", folder);
        }
    }
}
