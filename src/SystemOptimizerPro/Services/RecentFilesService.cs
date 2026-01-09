using System.IO;
using SystemOptimizerPro.Helpers;
using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services;

public class RecentFilesService
{
    private readonly string[] _recentLocations;

    public RecentFilesService()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _recentLocations = new[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.Recent),
            Path.Combine(appData, @"Microsoft\Windows\Recent\AutomaticDestinations"),
            Path.Combine(appData, @"Microsoft\Windows\Recent\CustomDestinations")
        };
    }

    public async Task<RecentFilesInfo> GetRecentFilesInfoAsync()
    {
        return await Task.Run(() =>
        {
            var info = new RecentFilesInfo();

            foreach (var location in _recentLocations)
            {
                if (!Directory.Exists(location)) continue;

                try
                {
                    var files = Directory.GetFiles(location);
                    info.TotalCount += files.Length;
                    info.TotalSizeBytes += files.Sum(f =>
                    {
                        try { return new FileInfo(f).Length; }
                        catch { return 0; }
                    });
                }
                catch
                {
                    // Access denied or other error
                }
            }

            return info;
        });
    }

    public async Task<List<RecentFileItem>> GetRecentFilesListAsync(int maxItems = 50)
    {
        return await Task.Run(() =>
        {
            var items = new List<RecentFileItem>();
            string recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            if (!Directory.Exists(recentFolder))
                return items;

            try
            {
                var files = Directory.GetFiles(recentFolder, "*.lnk")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.LastWriteTime)
                    .Take(maxItems);

                foreach (var file in files)
                {
                    items.Add(new RecentFileItem
                    {
                        Name = Path.GetFileNameWithoutExtension(file.Name),
                        Path = file.FullName,
                        LastAccessed = file.LastWriteTime,
                        SizeBytes = file.Length
                    });
                }
            }
            catch
            {
                // Handle error
            }

            return items;
        });
    }

    public async Task<CleaningResult> ClearRecentFilesAsync()
    {
        return await Task.Run(() =>
        {
            var startTime = DateTime.Now;
            int deletedCount = 0;
            int failedCount = 0;
            long freedBytes = 0;

            foreach (var location in _recentLocations)
            {
                if (!Directory.Exists(location)) continue;

                try
                {
                    foreach (var file in Directory.GetFiles(location))
                    {
                        try
                        {
                            var fileInfo = new FileInfo(file);
                            freedBytes += fileInfo.Length;
                            File.Delete(file);
                            deletedCount++;
                        }
                        catch
                        {
                            failedCount++;
                        }
                    }
                }
                catch
                {
                    // Location access error
                }
            }

            return new CleaningResult
            {
                Success = deletedCount > 0,
                Operation = "Clear Recent Files",
                ItemsProcessed = deletedCount,
                ItemsFailed = failedCount,
                BytesFreed = (ulong)freedBytes,
                Message = $"Cleared {deletedCount} recent files ({ByteConverter.ToReadableSize((ulong)freedBytes)} freed)",
                Timestamp = DateTime.Now,
                Duration = DateTime.Now - startTime
            };
        });
    }
}
