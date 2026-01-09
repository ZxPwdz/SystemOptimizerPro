using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.ViewModels;

public partial class ProcessesViewModel : BaseViewModel
{
    private readonly IProcessService _processService;
    private readonly DispatcherTimer _refreshTimer;

    [ObservableProperty]
    private ObservableCollection<ProcessInfo> _processes = new();

    [ObservableProperty]
    private ProcessInfo? _selectedProcess;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _sortColumn = "WorkingSet";

    [ObservableProperty]
    private bool _sortDescending = true;

    [ObservableProperty]
    private int _processCount;

    [ObservableProperty]
    private string _totalMemoryUsage = "0 MB";

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ProcessesViewModel(IProcessService processService)
    {
        _processService = processService;

        // Setup auto-refresh timer
        _refreshTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(5)
        };
        _refreshTimer.Tick += async (s, e) => await RefreshProcessesAsync();
        _refreshTimer.Start();

        // Initial load
        _ = RefreshProcessesAsync();
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = RefreshProcessesAsync();
    }

    [RelayCommand]
    private async Task RefreshProcessesAsync()
    {
        if (IsRefreshing) return;

        IsRefreshing = true;

        try
        {
            var allProcesses = await _processService.GetProcessesAsync();

            // Filter by search text
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? allProcesses
                : allProcesses.Where(p =>
                    p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    p.Id.ToString().Contains(SearchText)).ToList();

            // Sort
            filtered = SortColumn switch
            {
                "Name" => SortDescending
                    ? filtered.OrderByDescending(p => p.Name).ToList()
                    : filtered.OrderBy(p => p.Name).ToList(),
                "Id" => SortDescending
                    ? filtered.OrderByDescending(p => p.Id).ToList()
                    : filtered.OrderBy(p => p.Id).ToList(),
                "CpuUsage" => SortDescending
                    ? filtered.OrderByDescending(p => p.CpuUsage).ToList()
                    : filtered.OrderBy(p => p.CpuUsage).ToList(),
                _ => SortDescending
                    ? filtered.OrderByDescending(p => p.WorkingSet).ToList()
                    : filtered.OrderBy(p => p.WorkingSet).ToList()
            };

            Processes = new ObservableCollection<ProcessInfo>(filtered);
            ProcessCount = Processes.Count;
            TotalMemoryUsage = FormatBytes(Processes.Sum(p => p.WorkingSet));
        }
        catch
        {
            StatusMessage = "Error refreshing processes";
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task EndProcessAsync()
    {
        if (SelectedProcess == null) return;

        try
        {
            var success = await _processService.TerminateProcessAsync(SelectedProcess.Id);
            StatusMessage = success
                ? $"Terminated {SelectedProcess.Name}"
                : $"Failed to terminate {SelectedProcess.Name}";
            await RefreshProcessesAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task EndProcessTreeAsync()
    {
        if (SelectedProcess == null) return;

        try
        {
            var success = await _processService.TerminateProcessTreeAsync(SelectedProcess.Id);
            StatusMessage = success
                ? $"Terminated {SelectedProcess.Name} and child processes"
                : $"Failed to terminate {SelectedProcess.Name}";
            await RefreshProcessesAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task OpenFileLocationAsync()
    {
        if (SelectedProcess == null) return;

        try
        {
            var path = await _processService.GetProcessPathAsync(SelectedProcess.Id);
            if (!string.IsNullOrEmpty(path))
            {
                var directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory))
                {
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
                }
            }
            else
            {
                StatusMessage = "Cannot access process location";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    private void SortBy(string? column)
    {
        if (string.IsNullOrEmpty(column)) return;

        if (SortColumn == column)
        {
            SortDescending = !SortDescending;
        }
        else
        {
            SortColumn = column;
            SortDescending = true;
        }

        _ = RefreshProcessesAsync();
    }

    private static string FormatBytes(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB" };
        int i = 0;
        double value = bytes;
        while (value >= 1024 && i < suffixes.Length - 1)
        {
            value /= 1024;
            i++;
        }
        return $"{value:F1} {suffixes[i]}";
    }

    public override void Cleanup()
    {
        _refreshTimer.Stop();
    }
}
