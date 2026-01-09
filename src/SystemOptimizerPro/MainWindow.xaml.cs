using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;

namespace SystemOptimizerPro;

public partial class MainWindow : Window
{
    private bool _isClosing = false;
    private TaskbarIcon? _trayIcon;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _trayIcon = (TaskbarIcon)FindResource("TrayIconResource");
        if (_trayIcon != null)
        {
            _trayIcon.DataContext = DataContext;
        }
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            MaximizeButton_Click(sender, e);
        }
        else
        {
            DragMove();
        }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        // Minimize to system tray
        Hide();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
            MaximizeIcon.Text = "\uE922"; // Maximize icon
        }
        else
        {
            WindowState = WindowState.Maximized;
            MaximizeIcon.Text = "\uE923"; // Restore icon
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        _isClosing = true;
        _trayIcon?.Dispose();
        Close();
    }

    private void Window_StateChanged(object sender, System.EventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            MaximizeIcon.Text = "\uE923"; // Restore icon
        }
        else
        {
            MaximizeIcon.Text = "\uE922"; // Maximize icon
        }
    }

    private void TrayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        ShowWindow();
    }

    private void ShowMenuItem_Click(object sender, RoutedEventArgs e)
    {
        ShowWindow();
    }

    private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
    {
        _isClosing = true;
        _trayIcon?.Dispose();
        Close();
    }

    private void ShowWindow()
    {
        Show();
        WindowState = WindowState.Normal;
        Activate();
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        if (!_isClosing)
        {
            e.Cancel = true;
            Hide();
        }
        else
        {
            base.OnClosing(e);
        }
    }
}
