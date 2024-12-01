using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Converters;


public class FirstNotNullOrNullMultiConverter : IMultiValueConverter
{
    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        foreach (var item in values)
        {
            if (item is not null)
                return item;
        }

        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
