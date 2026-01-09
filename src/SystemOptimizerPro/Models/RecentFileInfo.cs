using SystemOptimizerPro.Helpers;

namespace SystemOptimizerPro.Models;

public class RecentFilesInfo
{
    public int TotalCount { get; set; }
    public long TotalSizeBytes { get; set; }
    public string TotalSizeFormatted => ByteConverter.ToReadableSize((ulong)TotalSizeBytes);
}

public class RecentFileItem
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime LastAccessed { get; set; }
    public long SizeBytes { get; set; }
    public string SizeFormatted => ByteConverter.ToReadableSize((ulong)SizeBytes);
}
