using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Converters;

public class StringFormatMultiConverter : IMultiValueConverter
{
    public static string Convert(string format, params object[] values)
    {
        return string.Format(format, values);
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException
            .ThrowIfNull(values, nameof(values));

        if (values.Length == 0)
            throw new ArgumentException("At least one value is required.", nameof(values));

        if (values[0] is not string format)
            throw new ArgumentException("First argument must be string format.", nameof(values));

        return Convert(format, values[1..]);
    }


    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}