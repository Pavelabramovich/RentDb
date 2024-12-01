using LilaRent.Domain.Fields;
using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class TimeUnitToMultiplierStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeUnit timeUnit)
            throw new ArgumentException($"Value should be {typeof(TimeUnit).Name}.", nameof(value));

        string stringUnit = timeUnit switch
        {
            TimeUnit.Minutes => "minute",
            TimeUnit.Hours => "hour",
            TimeUnit.Days => "day",

            _ => throw new NotSupportedException()
        };

        int multiplier = timeUnit.GetRentTimeMultiplier();

        if (multiplier == 1)
        {
            return stringUnit;
        }
        else
        {
            return $"{multiplier} {stringUnit}s";
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
