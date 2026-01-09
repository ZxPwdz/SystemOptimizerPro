using System.IO;
using System.Text.Json;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Services.Interfaces;

namespace SystemOptimizerPro.Services;

public class SettingsService : ISettingsService
{
    private readonly string _settingsPath;
    private AppSettings _settings;

    public SettingsService()
    {
        string appDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SystemOptimizerPro");

        Directory.CreateDirectory(appDataFolder);
        _settingsPath = Path.Combine(appDataFolder, "settings.json");
        _settings = LoadSettings();
    }

    public AppSettings GetSettings()
    {
        return _settings;
    }

    public void SaveSettings(AppSettings settings)
    {
        _settings = settings;
        try
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_settingsPath, json);
        }
        catch
        {
            // Handle save error
        }
    }

    public void UpdateSetting<T>(string propertyName, T value)
    {
        var property = typeof(AppSettings).GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            property.SetValue(_settings, value);
            SaveSettings(_settings);
        }
    }

    public T? GetSetting<T>(string propertyName)
    {
        var property = typeof(AppSettings).GetProperty(propertyName);
        if (property != null && property.CanRead)
        {
            var value = property.GetValue(_settings);
            if (value is T typedValue)
            {
                return typedValue;
            }
        }
        return default;
    }

    private AppSettings LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsPath))
            {
                var json = File.ReadAllText(_settingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch
        {
            // Handle load error
        }

        return new AppSettings();
    }
}
