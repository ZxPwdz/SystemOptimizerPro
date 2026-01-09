using SystemOptimizerPro.Helpers;

namespace SystemOptimizerPro.Models;

public class CleaningResult
{
    public bool Success { get; set; }
    public string Operation { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int ItemsProcessed { get; set; }
    public int ItemsFailed { get; set; }
    public ulong BytesFreed { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public TimeSpan Duration { get; set; }

    public string BytesFreedFormatted => ByteConverter.ToReadableSize(BytesFreed);
}

public class StandbyListPurgedEventArgs : EventArgs
{
    public bool Success { get; set; }
    public ulong MemoryFreedBytes { get; set; }
    public ulong PreviousStandbySize { get; set; }
    public ulong NewStandbySize { get; set; }
    public DateTime Timestamp { get; set; }

    public string MemoryFreedFormatted => ByteConverter.ToReadableSize(MemoryFreedBytes);
}
