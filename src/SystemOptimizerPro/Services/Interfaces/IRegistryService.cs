using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services.Interfaces;

public interface IRegistryService
{
    Task<List<RegistryIssue>> ScanForIssuesAsync(IProgress<int>? progress = null);
    Task<string> CreateBackupAsync();
    Task<CleaningResult> CleanIssuesAsync(List<RegistryIssue> issues);
    Task<bool> RestoreBackupAsync(string backupPath);
    Task<List<string>> GetBackupsAsync();
}
