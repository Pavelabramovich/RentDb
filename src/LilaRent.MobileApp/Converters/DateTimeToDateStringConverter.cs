using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class DateTimeToDateStringConverter : IValueConverter
{
    public static string Convert(DateTime dateTime)
    {
        return dateTime.ToString("dd.MM.yyyy");
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException
            .ThrowIfNull(value, nameof(value));

        try
        {
            var dateTime = System.Convert.ToDateTime(value);

            return Convert(dateTime);
        }
        catch (Exception ex) when (ex 
            is FormatException
            or InvalidCastException
            or OverflowException)
        {
            throw new ArgumentException("Value must be a datetime convertible", nameof(value), ex);
        }
    }


    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}