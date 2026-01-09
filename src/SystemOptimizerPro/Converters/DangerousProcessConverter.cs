using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemOptimizerPro.Converters;

public class DangerousProcessConverter : IValueConverter
{
    private static readonly SolidColorBrush DangerousBrush = new(Color.FromArgb(40, 220, 53, 69)); // Light red
    private static readonly SolidColorBrush NormalBrush = new(Colors.Transparent);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isDangerous && isDangerous)
        {
            return DangerousBrush;
        }
        return NormalBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
