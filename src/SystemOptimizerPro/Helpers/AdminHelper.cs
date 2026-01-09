using System.Diagnostics;
using System.Security.Principal;

namespace SystemOptimizerPro.Helpers;

public static class AdminHelper
{
    public static bool IsRunningAsAdmin()
    {
        using var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    public static void RestartAsAdmin()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = Environment.ProcessPath ?? Process.GetCurrentProcess().MainModule?.FileName,
            UseShellExecute = true,
            Verb = "runas"
        };

        try
        {
            Process.Start(startInfo);
            Environment.Exit(0);
        }
        catch
        {
            // User cancelled UAC prompt or error occurred
        }
    }

    public static bool EnsureAdmin()
    {
        if (!IsRunningAsAdmin())
        {
            RestartAsAdmin();
            return false;
        }
        return true;
    }
}
