using SystemOptimizerPro.Helpers;

namespace SystemOptimizerPro.Models;

public class ProcessInfo
{
    private static readonly HashSet<string> DangerousProcesses = new(StringComparer.OrdinalIgnoreCase)
    {
        // Windows Core System
        "System", "smss", "csrss", "wininit", "services", "lsass", "lsaiso",
        "svchost", "winlogon", "dwm", "explorer", "fontdrvhost", "sihost",
        "taskhostw", "RuntimeBroker", "ShellExperienceHost", "SearchHost",
        "StartMenuExperienceHost", "TextInputHost", "ctfmon", "conhost",
        "WmiPrvSE", "dllhost", "msdtc", "spoolsv", "wuauserv",

        // Windows Security
        "MsMpEng", "NisSrv", "SecurityHealthService", "SecurityHealthSystray",
        "SgrmBroker", "MpDefenderCoreService",

        // Critical Services
        "audiodg", "SearchIndexer", "SettingSyncHost", "SystemSettings",
        "ApplicationFrameHost", "WUDFHost", "dasHost", "Memory Compression",

        // Graphics & Display
        "dwm", "fontdrvhost",

        // Network
        "lsass", "netsh", "ipconfig"
    };

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double CpuUsage { get; set; }
    public long WorkingSet { get; set; }
    public long PrivateBytes { get; set; }
    public int ThreadCount { get; set; }
    public int HandleCount { get; set; }
    public string Status { get; set; } = "Running";
    public DateTime StartTime { get; set; }
    public string? FilePath { get; set; }
    public string? UserName { get; set; }

    public string MemoryFormatted => ByteConverter.ToReadableSize((ulong)WorkingSet);
    public string PrivateBytesFormatted => ByteConverter.ToReadableSize((ulong)PrivateBytes);
    public double MemoryMB => ByteConverter.BytesToMegabytes((ulong)WorkingSet);

    public bool IsDangerous => DangerousProcesses.Contains(Name) ||
                               Name.StartsWith("svchost", StringComparison.OrdinalIgnoreCase) ||
                               Id <= 4; // System Idle Process (0) and System (4)
}
