using System.Runtime.InteropServices;

namespace SystemOptimizerPro.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct MEMORYSTATUSEX
{
    internal uint dwLength;
    internal uint dwMemoryLoad;
    internal ulong ullTotalPhys;
    internal ulong ullAvailPhys;
    internal ulong ullTotalPageFile;
    internal ulong ullAvailPageFile;
    internal ulong ullTotalVirtual;
    internal ulong ullAvailVirtual;
    internal ulong ullAvailExtendedVirtual;

    public static MEMORYSTATUSEX Create()
    {
        return new MEMORYSTATUSEX
        {
            dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>()
        };
    }
}
