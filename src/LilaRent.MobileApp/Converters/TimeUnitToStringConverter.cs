using LilaRent.Domain.Fields;
using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class TimeStepToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ((TimeUnit)value).ToString().ToLower();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Enum.Parse<TimeUnit>(value.ToString());
    }
}
