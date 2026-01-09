using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services.Interfaces;

public interface ISettingsService
{
    AppSettings GetSettings();
    void SaveSettings(AppSettings settings);
    void UpdateSetting<T>(string propertyName, T value);
    T? GetSetting<T>(string propertyName);
}
