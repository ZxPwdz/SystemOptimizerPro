using System.Diagnostics;
using System.Runtime.InteropServices;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Native;
using SystemOptimizerPro.Native.Structs;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.Services;

public class MemoryService : IMemoryService
{
    public Task<MemoryInfo> GetMemoryInfoAsync()
    {
        return Task.Run(() =>
        {
            var memStatus = MEMORYSTATUSEX.Create();

            if (!NativeMethods.GlobalMemoryStatusEx(ref memStatus))
                throw new InvalidOperationException("Failed to get memory status");

            var perfInfo = PERFORMANCE_INFORMATION.Create();

            if (!NativeMethods.GetPerformanceInfo(out perfInfo, perfInfo.cb))
                throw new InvalidOperationException("Failed to get performance info");

            ulong pageSize = (ulong)perfInfo.PageSize;
            ulong totalPhysical = memStatus.ullTotalPhys;
            ulong availablePhysical = memStatus.ullAvailPhys;
            ulong systemCache = (ulong)perfInfo.SystemCache * pageSize;

            // Calculate memory breakdown
            ulong usedPhysical = totalPhysical - availablePhysical;

            // Standby is part of available (cached but reclaimable)
            // This is a simplified calculation
            ulong standby = systemCache;
            ulong freeMemory = availablePhysical > standby ? availablePhysical - standby : 0;

            return new MemoryInfo
            {
                TotalPhysical = totalPhysical,
                AvailablePhysical = availablePhysical,
                UsedPhysical = usedPhysical,
                StandbyList = standby,
                FreeMemory = freeMemory,
                UsagePercent = memStatus.dwMemoryLoad,
                PageSize = pageSize,
                ProcessCount = perfInfo.ProcessCount,
                ThreadCount = perfInfo.ThreadCount,
                HandleCount = perfInfo.HandleCount
            };
        });
    }

    public Task<bool> PurgeStandbyListAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                if (!EnablePrivilege(NativeMethods.SE_PROF_SINGLE_PROCESS_NAME))
                    return false;

                int command = (int)SYSTEM_MEMORY_LIST_COMMAND.MemoryPurgeStandbyList;
                int size = Marshal.SizeOf<int>();
                IntPtr buffer = Marshal.AllocHGlobal(size);

                try
                {
                    Marshal.WriteInt32(buffer, command);
                    int result = NativeMethods.NtSetSystemInformation(
                        SYSTEM_INFORMATION_CLASS.SystemMemoryListInformation,
                        buffer,
                        size);

                    return result == 0; // STATUS_SUCCESS
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch
            {
                return false;
            }
        });
    }

    public Task<bool> EmptyWorkingSetsAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                if (!EnablePrivilege(NativeMethods.SE_PROF_SINGLE_PROCESS_NAME))
                    return false;

                int command = (int)SYSTEM_MEMORY_LIST_COMMAND.MemoryEmptyWorkingSets;
                int size = Marshal.SizeOf<int>();
                IntPtr buffer = Marshal.AllocHGlobal(size);

                try
                {
                    Marshal.WriteInt32(buffer, command);
                    int result = NativeMethods.NtSetSystemInformation(
                        SYSTEM_INFORMATION_CLASS.SystemMemoryListInformation,
                        buffer,
                        size);

                    return result == 0;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
            catch
            {
                return false;
            }
        });
    }

    public Task<ulong> GetStandbyListSizeAsync()
    {
        return Task.Run(async () =>
        {
            var memInfo = await GetMemoryInfoAsync();
            return memInfo.StandbyList;
        });
    }

    private static bool EnablePrivilege(string privilegeName)
    {
        IntPtr tokenHandle = IntPtr.Zero;

        try
        {
            IntPtr currentProcess = Process.GetCurrentProcess().Handle;

            if (!NativeMethods.OpenProcessToken(currentProcess,
                NativeMethods.TOKEN_ADJUST_PRIVILEGES | NativeMethods.TOKEN_QUERY,
                out tokenHandle))
                return false;

            if (!NativeMethods.LookupPrivilegeValue(null, privilegeName, out LUID luid))
                return false;

            var tokenPrivileges = new TOKEN_PRIVILEGES
            {
                PrivilegeCount = 1,
                Privileges = new LUID_AND_ATTRIBUTES
                {
                    Luid = luid,
                    Attributes = NativeMethods.SE_PRIVILEGE_ENABLED
                }
            };

            return NativeMethods.AdjustTokenPrivileges(
                tokenHandle,
                false,
                ref tokenPrivileges,
                0,
                IntPtr.Zero,
                IntPtr.Zero);
        }
        finally
        {
            if (tokenHandle != IntPtr.Zero)
                NativeMethods.CloseHandle(tokenHandle);
        }
    }
}
