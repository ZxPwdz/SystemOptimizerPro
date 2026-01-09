using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SystemOptimizerPro.Services;
using SystemOptimizerPro.Services.Interfaces;
using SystemOptimizerPro.ViewModels;

namespace SystemOptimizerPro;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Services
        services.AddSingleton<IMemoryService, MemoryService>();
        services.AddSingleton<IProcessService, ProcessService>();
        services.AddSingleton<IRegistryService, RegistryCleanerService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<StandbyListService>();
        services.AddSingleton<DnsCacheService>();
        services.AddSingleton<RecentFilesService>();
        services.AddSingleton<StartupService>();

        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<MemoryCleanerViewModel>();
        services.AddTransient<ProcessesViewModel>();
        services.AddTransient<CleaningToolsViewModel>();
        services.AddTransient<SettingsViewModel>();

        // Main Window
        services.AddSingleton<MainWindow>();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.Show();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public static IServiceProvider GetServiceProvider()
    {
        return ((App)Current)._serviceProvider;
    }
}
