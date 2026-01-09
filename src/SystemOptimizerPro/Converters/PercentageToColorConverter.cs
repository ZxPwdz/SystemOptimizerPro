using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemOptimizerPro.Converters;

public class PercentageToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double percentage = 0;

        if (value is double d)
            percentage = d;
        else if (value is int i)
            percentage = i;
        else if (value is uint u)
            percentage = u;

        // Get colors from resources
        var lowColor = (Brush)Application.Current.Resources["AccentSecondaryBrush"];
        var mediumColor = (Brush)Application.Current.Resources["AccentWarningBrush"];
        var highColor = (Brush)Application.Current.Resources["AccentDangerBrush"];

        return percentage switch
        {
            < 50 => lowColor,
            < 80 => mediumColor,
            _ => highColor
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
