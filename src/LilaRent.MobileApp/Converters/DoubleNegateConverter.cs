using System.Globalization;


namespace LilaRent.MobileApp.Converters;


/// <include file='Documentation/Converters/DoubleNegateConverter.xml' path='doc/class[@name="DoubleNegateConverter"]/description' />
public class DoubleNegateConverter : IValueConverter
{
    public static double Convert(double @double)
    {
        return -@double;
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException
            .ThrowIfNull(value, nameof(value));

        try
        {
            var @double = System.Convert.ToDouble(value);

            return Convert(@double);
        }
        catch (Exception ex) when (ex 
            is FormatException
            or InvalidCastException
            or OverflowException)
        {
            throw new ArgumentException("Value must be a double convertible", nameof(value), ex);
        }
    }


    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}
