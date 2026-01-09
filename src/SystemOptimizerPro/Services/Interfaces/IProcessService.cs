using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services.Interfaces;

public interface IProcessService
{
    Task<List<ProcessInfo>> GetProcessesAsync();
    Task<double> GetCpuUsageAsync();
    Task<bool> TerminateProcessAsync(int processId);
    Task<bool> TerminateProcessTreeAsync(int processId);
    Task<string?> GetProcessPathAsync(int processId);
}
