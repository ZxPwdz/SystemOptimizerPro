using System.Diagnostics;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Native;

namespace SystemOptimizerPro.Services;

public class DnsCacheService
{
    public async Task<CleaningResult> FlushDnsCacheAsync()
    {
        return await Task.Run(() =>
        {
            var startTime = DateTime.Now;

            try
            {
                // Method 1: Use native API
                bool success = NativeMethods.DnsFlushResolverCache();

                if (!success)
                {
                    // Method 2: Fallback to ipconfig
                    var psi = new ProcessStartInfo
                    {
                        FileName = "ipconfig",
                        Arguments = "/flushdns",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true
                    };

                    using var process = Process.Start(psi);
                    process?.WaitForExit(5000);
                    success = process?.ExitCode == 0;
                }

                return new CleaningResult
                {
                    Success = success,
                    Operation = "DNS Cache Flush",
                    Message = success ? "DNS cache flushed successfully" : "Failed to flush DNS cache",
                    Timestamp = DateTime.Now,
                    Duration = DateTime.Now - startTime
                };
            }
            catch (Exception ex)
            {
                return new CleaningResult
                {
                    Success = false,
                    Operation = "DNS Cache Flush",
                    Message = $"Error: {ex.Message}",
                    Timestamp = DateTime.Now,
                    Duration = DateTime.Now - startTime
                };
            }
        });
    }

    public async Task<int> GetCacheEntryCountAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "ipconfig",
                    Arguments = "/displaydns",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                };

                using var process = Process.Start(psi);
                string output = process?.StandardOutput.ReadToEnd() ?? "";
                process?.WaitForExit(10000);

                // Count "Record Name" occurrences
                int count = 0;
                int index = 0;
                while ((index = output.IndexOf("Record Name", index, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    count++;
                    index++;
                }

                return count;
            }
            catch
            {
                return -1;
            }
        });
    }
}
