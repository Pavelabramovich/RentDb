using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class ConverterGroup : List<IValueConverter>, IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return this.Aggregate(value, (current, converter) =>
        {
            return converter.Convert(current, targetType, parameter, culture);
        });
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return this.AsEnumerable().Reverse().Aggregate(value, (current, converter) => 
        { 
            return converter.ConvertBack(current, targetType, parameter, culture); 
        });
    }
}
