using CommunityToolkit.Mvvm.ComponentModel;

namespace SystemOptimizerPro.ViewModels;

public abstract class BaseViewModel : ObservableObject
{
    private bool _isLoading;
    private string _loadingMessage = string.Empty;

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public string LoadingMessage
    {
        get => _loadingMessage;
        set => SetProperty(ref _loadingMessage, value);
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void Cleanup()
    {
    }
}
