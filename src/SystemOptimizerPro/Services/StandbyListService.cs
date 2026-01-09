using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.Services;

public class StandbyListService : IDisposable
{
    private readonly IMemoryService _memoryService;
    private readonly ISettingsService _settingsService;
    private CancellationTokenSource? _monitorCts;
    private Task? _monitorTask;

    public event EventHandler<StandbyListPurgedEventArgs>? StandbyListPurged;
    public event EventHandler<MemoryInfo>? MemoryUpdated;

    public bool IsMonitoring { get; private set; }
    public int PurgeCount { get; private set; }

    public StandbyListService(IMemoryService memoryService, ISettingsService settingsService)
    {
        _memoryService = memoryService;
        _settingsService = settingsService;
    }

    public async Task<StandbyListPurgedEventArgs> PurgeNowAsync()
    {
        var beforeInfo = await _memoryService.GetMemoryInfoAsync();

        bool success = await _memoryService.PurgeStandbyListAsync();

        // Wait a moment for memory to be reclaimed
        await Task.Delay(100);

        var afterInfo = await _memoryService.GetMemoryInfoAsync();

        ulong freed = afterInfo.AvailablePhysical > beforeInfo.AvailablePhysical
            ? afterInfo.AvailablePhysical - beforeInfo.AvailablePhysical
            : 0;

        var result = new StandbyListPurgedEventArgs
        {
            Success = success,
            MemoryFreedBytes = freed,
            PreviousStandbySize = beforeInfo.StandbyList,
            NewStandbySize = afterInfo.StandbyList,
            Timestamp = DateTime.Now
        };

        if (success)
        {
            PurgeCount++;
            StandbyListPurged?.Invoke(this, result);
        }

        return result;
    }

    public void StartMonitoring()
    {
        if (IsMonitoring) return;

        _monitorCts = new CancellationTokenSource();
        _monitorTask = MonitorLoopAsync(_monitorCts.Token);
        IsMonitoring = true;
    }

    public void StopMonitoring()
    {
        if (!IsMonitoring) return;

        _monitorCts?.Cancel();
        IsMonitoring = false;
    }

    private async Task MonitorLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var settings = _settingsService.GetSettings();
                var memInfo = await _memoryService.GetMemoryInfoAsync();
                MemoryUpdated?.Invoke(this, memInfo);

                // Check if purge conditions are met
                if (settings.AutoPurgeEnabled)
                {
                    ulong standbyThreshold = (ulong)settings.StandbyThresholdMB * 1024 * 1024;
                    ulong freeThreshold = (ulong)settings.FreeMemoryThresholdMB * 1024 * 1024;

                    bool standbyExceedsThreshold = memInfo.StandbyList >= standbyThreshold;
                    bool freeMemoryBelowThreshold = memInfo.AvailablePhysical <= freeThreshold;

                    if (standbyExceedsThreshold && freeMemoryBelowThreshold)
                    {
                        await PurgeNowAsync();
                    }
                }

                await Task.Delay(settings.PollingRateMs, ct);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Monitor error: {ex.Message}");
                await Task.Delay(1000, ct);
            }
        }
    }

    public void Dispose()
    {
        StopMonitoring();
        _monitorCts?.Dispose();
    }
}
