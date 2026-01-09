using System.Globalization;
using System.Windows.Data;

namespace SystemOptimizerPro.Converters;

public class BytesToGigabytesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ulong bytes)
        {
            return bytes / (1024.0 * 1024.0 * 1024.0);
        }
        if (value is long bytesLong)
        {
            return bytesLong / (1024.0 * 1024.0 * 1024.0);
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double gb)
        {
            return (ulong)(gb * 1024 * 1024 * 1024);
        }
        return 0UL;
    }
}
