using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class CleaningToolsViewModel : BaseViewModel
{
    private readonly DnsCacheService _dnsCacheService;
    private readonly RecentFilesService _recentFilesService;
    private readonly IRegistryService _registryService;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private int _dnsCacheEntries;

    [ObservableProperty]
    private bool _isDnsLoading;

    [ObservableProperty]
    private string _dnsStatus = string.Empty;

    [ObservableProperty]
    private int _recentFilesCount;

    [ObservableProperty]
    private string _recentFilesSize = "0 B";

    [ObservableProperty]
    private bool _isRecentFilesLoading;

    [ObservableProperty]
    private string _recentFilesStatus = string.Empty;

    [ObservableProperty]
    private ObservableCollection<RegistryIssue> _registryIssues = new();

    [ObservableProperty]
    private int _registryIssueCount;

    [ObservableProperty]
    private bool _isRegistryScanning;

    [ObservableProperty]
    private int _registryScanProgress;

    [ObservableProperty]
    private string _registryStatus = string.Empty;

    [ObservableProperty]
    private bool _selectAllIssues = true;

    public CleaningToolsViewModel(
        DnsCacheService dnsCacheService,
        RecentFilesService recentFilesService,
        IRegistryService registryService,
        ISettingsService settingsService)
    {
        _dnsCacheService = dnsCacheService;
        _recentFilesService = recentFilesService;
        _registryService = registryService;
        _settingsService = settingsService;

        // Initial load
        _ = InitializeAsync();
    }

    public override async Task InitializeAsync()
    {
        await RefreshDnsCacheAsync();
        await RefreshRecentFilesAsync();
    }

    partial void OnSelectAllIssuesChanged(bool value)
    {
        foreach (var issue in RegistryIssues)
        {
            issue.IsSelected = value;
        }
    }

    #region DNS Cache

    [RelayCommand]
    private async Task RefreshDnsCacheAsync()
    {
        IsDnsLoading = true;
        try
        {
            DnsCacheEntries = await _dnsCacheService.GetCacheEntryCountAsync();
            DnsStatus = $"Last checked: {DateTime.Now:HH:mm}";
        }
        catch
        {
            DnsStatus = "Error checking DNS cache";
        }
        finally
        {
            IsDnsLoading = false;
        }
    }

    [RelayCommand]
    private async Task FlushDnsCacheAsync()
    {
        IsDnsLoading = true;
        try
        {
            var result = await _dnsCacheService.FlushDnsCacheAsync();
            DnsStatus = result.Success ? "DNS cache flushed successfully" : "Failed to flush DNS cache";
            var settings = _settingsService.GetSettings();
            settings.LastDnsFlush = DateTime.Now;
            _settingsService.SaveSettings(settings);
            await RefreshDnsCacheAsync();
        }
        catch (Exception ex)
        {
            DnsStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsDnsLoading = false;
        }
    }

    #endregion

    #region Recent Files

    [RelayCommand]
    private async Task RefreshRecentFilesAsync()
    {
        IsRecentFilesLoading = true;
        try
        {
            var info = await _recentFilesService.GetRecentFilesInfoAsync();
            RecentFilesCount = info.TotalCount;
            RecentFilesSize = info.TotalSizeFormatted;
            RecentFilesStatus = $"Last checked: {DateTime.Now:HH:mm}";
        }
        catch
        {
            RecentFilesStatus = "Error checking recent files";
        }
        finally
        {
            IsRecentFilesLoading = false;
        }
    }

    [RelayCommand]
    private async Task ClearRecentFilesAsync()
    {
        IsRecentFilesLoading = true;
        try
        {
            var result = await _recentFilesService.ClearRecentFilesAsync();
            RecentFilesStatus = result.Message;
            var settings = _settingsService.GetSettings();
            settings.LastRecentFilesClear = DateTime.Now;
            _settingsService.SaveSettings(settings);
            await RefreshRecentFilesAsync();
        }
        catch (Exception ex)
        {
            RecentFilesStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsRecentFilesLoading = false;
        }
    }

    #endregion

    #region Registry

    [RelayCommand]
    private async Task ScanRegistryAsync()
    {
        IsRegistryScanning = true;
        RegistryScanProgress = 0;
        RegistryStatus = "Scanning...";

        try
        {
            var progress = new Progress<int>(p => RegistryScanProgress = p);
            var issues = await _registryService.ScanForIssuesAsync(progress);
            RegistryIssues = new ObservableCollection<RegistryIssue>(issues);
            RegistryIssueCount = issues.Count;
            RegistryStatus = $"Found {issues.Count} issues";
        }
        catch (Exception ex)
        {
            RegistryStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsRegistryScanning = false;
            RegistryScanProgress = 100;
        }
    }

    [RelayCommand]
    private async Task CleanRegistryAsync()
    {
        var selectedIssues = RegistryIssues.Where(i => i.IsSelected).ToList();
        if (selectedIssues.Count == 0)
        {
            RegistryStatus = "No issues selected";
            return;
        }

        IsRegistryScanning = true;
        RegistryStatus = "Creating backup...";

        try
        {
            // Create backup first
            var backupPath = await _registryService.CreateBackupAsync();
            RegistryStatus = "Cleaning selected issues...";

            // Clean issues
            var result = await _registryService.CleanIssuesAsync(selectedIssues);
            RegistryStatus = result.Message;

            var settings = _settingsService.GetSettings();
            settings.LastRegistryClean = DateTime.Now;
            _settingsService.SaveSettings(settings);

            // Rescan
            await ScanRegistryAsync();
        }
        catch (Exception ex)
        {
            RegistryStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsRegistryScanning = false;
        }
    }

    [RelayCommand]
    private async Task RestoreBackupAsync()
    {
        try
        {
            var backups = await _registryService.GetBackupsAsync();
            if (backups.Count == 0)
            {
                RegistryStatus = "No backups found";
                return;
            }

            // Restore most recent backup
            var success = await _registryService.RestoreBackupAsync(backups[0]);
            RegistryStatus = success ? "Backup restored successfully" : "Failed to restore backup";
        }
        catch (Exception ex)
        {
            RegistryStatus = $"Error: {ex.Message}";
        }
    }

    #endregion
}
