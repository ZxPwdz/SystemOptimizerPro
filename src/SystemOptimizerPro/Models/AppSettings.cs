namespace SystemOptimizerPro.Models;

public class AppSettings
{
    // Auto-purge settings
    public bool AutoPurgeEnabled { get; set; } = false;
    public int StandbyThresholdMB { get; set; } = 1024; // 1 GB
    public int FreeMemoryThresholdMB { get; set; } = 1024; // 1 GB
    public int PollingRateMs { get; set; } = 1000; // 1 second

    // Startup settings
    public bool StartWithWindows { get; set; } = false;
    public bool StartMinimized { get; set; } = false;
    public bool MinimizeToTray { get; set; } = true;

    // Cleaning settings
    public bool ConfirmBeforeClean { get; set; } = true;
    public bool CreateRegistryBackup { get; set; } = true;

    // Timer resolution (advanced)
    public bool EnableTimerResolution { get; set; } = false;
    public double CustomTimerResolutionMs { get; set; } = 0.5;

    // UI settings
    public bool ShowNotifications { get; set; } = true;
    public int MonitoringRefreshRateMs { get; set; } = 1000;

    // Last cleanup times
    public DateTime? LastMemoryClean { get; set; }
    public DateTime? LastDnsFlush { get; set; }
    public DateTime? LastRecentFilesClear { get; set; }
    public DateTime? LastRegistryClean { get; set; }
}
