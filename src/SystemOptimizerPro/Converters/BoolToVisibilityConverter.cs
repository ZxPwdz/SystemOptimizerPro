using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SystemOptimizerPro.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = false;

        if (value is bool b)
            boolValue = b;
        else if (value is int i)
            boolValue = i != 0;

        // Check if we should invert
        bool invert = parameter?.ToString()?.ToLower() == "invert";

        if (invert)
            boolValue = !boolValue;

        return boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            bool result = visibility == Visibility.Visible;

            bool invert = parameter?.ToString()?.ToLower() == "invert";
            if (invert)
                result = !result;

            return result;
        }
        return false;
    }
}
