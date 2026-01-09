using System.Collections.ObjectModel;
using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services;

public class ActionLogService
{
    private const int MaxLogEntries = 20;

    public ObservableCollection<ActionLogEntry> ActionLog { get; } = new();

    public void AddEntry(string action, string details, bool success)
    {
        var entry = new ActionLogEntry
        {
            Timestamp = DateTime.Now,
            Action = action,
            Details = details,
            Success = success
        };

        // Insert at the beginning (newest first)
        ActionLog.Insert(0, entry);

        // Keep only the most recent entries
        while (ActionLog.Count > MaxLogEntries)
        {
            ActionLog.RemoveAt(ActionLog.Count - 1);
        }
    }

    public void Clear()
    {
        ActionLog.Clear();
    }
}
