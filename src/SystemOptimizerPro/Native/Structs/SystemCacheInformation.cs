using System.Runtime.InteropServices;

namespace SystemOptimizerPro.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct SYSTEM_CACHE_INFORMATION
{
    internal UIntPtr CurrentSize;
    internal UIntPtr PeakSize;
    internal uint PageFaultCount;
    internal UIntPtr MinimumWorkingSet;
    internal UIntPtr MaximumWorkingSet;
    internal UIntPtr CurrentSizeIncludingTransitionInPages;
    internal UIntPtr PeakSizeIncludingTransitionInPages;
    internal uint TransitionRePurposeCount;
    internal uint Flags;
}

[StructLayout(LayoutKind.Sequential)]
internal struct SYSTEM_FILECACHE_INFORMATION
{
    internal UIntPtr CurrentSize;
    internal UIntPtr PeakSize;
    internal uint PageFaultCount;
    internal UIntPtr MinimumWorkingSet;
    internal UIntPtr MaximumWorkingSet;
    internal UIntPtr CurrentSizeIncludingTransitionInPages;
    internal UIntPtr PeakSizeIncludingTransitionInPages;
    internal uint TransitionRePurposeCount;
    internal uint Flags;
}

[StructLayout(LayoutKind.Sequential)]
internal struct LUID
{
    internal uint LowPart;
    internal int HighPart;
}

[StructLayout(LayoutKind.Sequential)]
internal struct LUID_AND_ATTRIBUTES
{
    internal LUID Luid;
    internal uint Attributes;
}

[StructLayout(LayoutKind.Sequential)]
internal struct TOKEN_PRIVILEGES
{
    internal uint PrivilegeCount;
    internal LUID_AND_ATTRIBUTES Privileges;
}
