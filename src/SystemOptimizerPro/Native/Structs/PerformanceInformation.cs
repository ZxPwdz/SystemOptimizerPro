using System.Runtime.InteropServices;

namespace SystemOptimizerPro.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct PERFORMANCE_INFORMATION
{
    internal uint cb;
    internal UIntPtr CommitTotal;
    internal UIntPtr CommitLimit;
    internal UIntPtr CommitPeak;
    internal UIntPtr PhysicalTotal;
    internal UIntPtr PhysicalAvailable;
    internal UIntPtr SystemCache;
    internal UIntPtr KernelTotal;
    internal UIntPtr KernelPaged;
    internal UIntPtr KernelNonpaged;
    internal UIntPtr PageSize;
    internal uint HandleCount;
    internal uint ProcessCount;
    internal uint ThreadCount;

    public static PERFORMANCE_INFORMATION Create()
    {
        return new PERFORMANCE_INFORMATION
        {
            cb = (uint)Marshal.SizeOf<PERFORMANCE_INFORMATION>()
        };
    }
}
