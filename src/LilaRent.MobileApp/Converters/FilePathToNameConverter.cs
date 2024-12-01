using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class FilePathToNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) 
            return null;

        string path = value.ToString()!;

        return Path.GetFileName(path);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}