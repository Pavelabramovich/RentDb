using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class ColorToTranslucentConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;


        var color = (Color)value;

        float newAlpha = Math.Max(color.Alpha - 0.4f, 0f);

        return new Color(color.Red, color.Green, color.Blue, newAlpha);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        var color = (Color)value;

        float newAlpha = Math.Min(color.Alpha + 0.4f, 1f);

        return new Color(color.Red, color.Green, color.Blue, newAlpha);
    }
}
