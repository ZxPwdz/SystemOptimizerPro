using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services.Interfaces;

public interface IMemoryService
{
    Task<MemoryInfo> GetMemoryInfoAsync();
    Task<bool> PurgeStandbyListAsync();
    Task<bool> EmptyWorkingSetsAsync();
    Task<ulong> GetStandbyListSizeAsync();
}
