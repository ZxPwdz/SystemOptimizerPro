using System.Diagnostics;
using System.Management;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.Services;

public class ProcessService : IProcessService
{
    private PerformanceCounter? _cpuCounter;
    private double _lastCpuUsage;

    public ProcessService()
    {
        try
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _cpuCounter.NextValue(); // First call always returns 0
        }
        catch
        {
            _cpuCounter = null;
        }
    }

    public Task<List<ProcessInfo>> GetProcessesAsync()
    {
        return Task.Run(() =>
        {
            var processes = new List<ProcessInfo>();
            var allProcesses = Process.GetProcesses();

            foreach (var process in allProcesses)
            {
                try
                {
                    var info = new ProcessInfo
                    {
                        Id = process.Id,
                        Name = process.ProcessName,
                        WorkingSet = process.WorkingSet64,
                        PrivateBytes = process.PrivateMemorySize64,
                        ThreadCount = process.Threads.Count,
                        HandleCount = process.HandleCount,
                        Status = process.Responding ? "Running" : "Not Responding"
                    };

                    try
                    {
                        info.StartTime = process.StartTime;
                    }
                    catch { }

                    try
                    {
                        info.FilePath = process.MainModule?.FileName;
                    }
                    catch { }

                    processes.Add(info);
                }
                catch
                {
                    // Skip processes we can't access
                }
                finally
                {
                    process.Dispose();
                }
            }

            return processes.OrderByDescending(p => p.WorkingSet).ToList();
        });
    }

    public Task<double> GetCpuUsageAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                if (_cpuCounter != null)
                {
                    _lastCpuUsage = _cpuCounter.NextValue();
                }
            }
            catch
            {
                // Return last known value on error
            }

            return Math.Round(_lastCpuUsage, 1);
        });
    }

    public Task<bool> TerminateProcessAsync(int processId)
    {
        return Task.Run(() =>
        {
            try
            {
                var process = Process.GetProcessById(processId);
                process.Kill();
                process.WaitForExit(5000);
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    public Task<bool> TerminateProcessTreeAsync(int processId)
    {
        return Task.Run(() =>
        {
            try
            {
                var process = Process.GetProcessById(processId);
                KillProcessTree(process);
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    private static void KillProcessTree(Process process)
    {
        try
        {
            // Get child processes using WMI
            using var searcher = new ManagementObjectSearcher(
                $"SELECT * FROM Win32_Process WHERE ParentProcessId={process.Id}");

            foreach (var obj in searcher.Get())
            {
                try
                {
                    var childProcess = Process.GetProcessById(Convert.ToInt32(obj["ProcessId"]));
                    KillProcessTree(childProcess);
                }
                catch { }
            }

            process.Kill();
            process.WaitForExit(5000);
        }
        catch { }
    }

    public Task<string?> GetProcessPathAsync(int processId)
    {
        return Task.Run(() =>
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return process.MainModule?.FileName;
            }
            catch
            {
                return null;
            }
        });
    }
}
