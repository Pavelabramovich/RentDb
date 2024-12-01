using System.Globalization;


namespace LilaRent.MobileApp.Converters;


internal class IsAnyNotNullMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.Any(v => v is not null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
