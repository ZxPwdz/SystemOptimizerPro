using SystemOptimizerPro.Helpers;

namespace SystemOptimizerPro.Models;

public class MemoryInfo
{
    public ulong TotalPhysical { get; set; }
    public ulong AvailablePhysical { get; set; }
    public ulong UsedPhysical { get; set; }
    public ulong StandbyList { get; set; }
    public ulong ModifiedList { get; set; }
    public ulong FreeMemory { get; set; }
    public uint UsagePercent { get; set; }
    public ulong PageSize { get; set; }
    public uint ProcessCount { get; set; }
    public uint ThreadCount { get; set; }
    public uint HandleCount { get; set; }

    // Formatted properties
    public string TotalPhysicalFormatted => ByteConverter.ToReadableSize(TotalPhysical);
    public string AvailablePhysicalFormatted => ByteConverter.ToReadableSize(AvailablePhysical);
    public string UsedPhysicalFormatted => ByteConverter.ToReadableSize(UsedPhysical);
    public string StandbyListFormatted => ByteConverter.ToReadableSize(StandbyList);
    public string FreeMemoryFormatted => ByteConverter.ToReadableSize(FreeMemory);

    public double TotalGB => ByteConverter.BytesToGigabytes(TotalPhysical);
    public double UsedGB => ByteConverter.BytesToGigabytes(UsedPhysical);
    public double AvailableGB => ByteConverter.BytesToGigabytes(AvailablePhysical);
    public double StandbyGB => ByteConverter.BytesToGigabytes(StandbyList);
    public double FreeGB => ByteConverter.BytesToGigabytes(FreeMemory);
}
