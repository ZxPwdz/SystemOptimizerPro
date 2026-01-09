namespace SystemOptimizerPro.Models;

public class RegistryIssue
{
    public RegistryIssueCategory Category { get; set; }
    public string KeyPath { get; set; } = string.Empty;
    public string? ValueName { get; set; }
    public string Description { get; set; } = string.Empty;
    public IssueSeverity Severity { get; set; }
    public bool IsSelected { get; set; } = true;

    public string CategoryDisplayName => Category switch
    {
        RegistryIssueCategory.FileAssociation => "File Association",
        RegistryIssueCategory.ObsoleteSoftware => "Obsolete Software",
        RegistryIssueCategory.SharedDll => "Shared DLL",
        RegistryIssueCategory.StartupEntry => "Startup Entry",
        RegistryIssueCategory.MruList => "Recent List (MRU)",
        _ => "Unknown"
    };

    public string SeverityDisplayName => Severity switch
    {
        IssueSeverity.Low => "Low",
        IssueSeverity.Medium => "Medium",
        IssueSeverity.High => "High",
        _ => "Unknown"
    };
}

public enum RegistryIssueCategory
{
    FileAssociation,
    ObsoleteSoftware,
    SharedDll,
    StartupEntry,
    MruList
}

public enum IssueSeverity
{
    Low,
    Medium,
    High
}
