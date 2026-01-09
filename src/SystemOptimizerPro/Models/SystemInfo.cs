namespace SystemOptimizerPro.Models;

public class SystemInfo
{
    public string OperatingSystem { get; set; } = string.Empty;
    public string OSVersion { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int ProcessorCount { get; set; }
    public string ProcessorName { get; set; } = string.Empty;
    public bool Is64BitOperatingSystem { get; set; }
    public TimeSpan SystemUptime { get; set; }

    public static SystemInfo GetCurrent()
    {
        return new SystemInfo
        {
            OperatingSystem = Environment.OSVersion.Platform.ToString(),
            OSVersion = Environment.OSVersion.VersionString,
            MachineName = Environment.MachineName,
            UserName = Environment.UserName,
            ProcessorCount = Environment.ProcessorCount,
            Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
            SystemUptime = TimeSpan.FromMilliseconds(Environment.TickCount64)
        };
    }
}

public class HealthItem
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int NumericValue { get; set; }
    public HealthStatus Status { get; set; }

    public string StatusColor => Status switch
    {
        HealthStatus.Good => "#34A853",      // Green
        HealthStatus.Warning => "#FBBC04",   // Orange/Yellow
        HealthStatus.Critical => "#EA4335",  // Red
        _ => "#9AA0A6"                       // Gray
    };

    public string ValueColor => Status switch
    {
        HealthStatus.Good => "#34A853",
        HealthStatus.Warning => "#FBBC04",
        HealthStatus.Critical => "#EA4335",
        _ => "#5F6368"
    };
}

public class ActionLogEntry
{
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public bool Success { get; set; }

    public string TimeDisplay => Timestamp.ToString("HH:mm:ss");
    public string StatusIcon => Success ? "\u2713" : "\u2717";
    public string StatusColor => Success ? "#34A853" : "#EA4335";
}

public enum HealthStatus
{
    Good,
    Warning,
    Critical,
    Unknown
}
