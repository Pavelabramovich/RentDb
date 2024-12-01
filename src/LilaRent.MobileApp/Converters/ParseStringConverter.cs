using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using SystemConvert = System.Convert;


namespace LilaRent.MobileApp.Converters;


public class ParseStringConverter<TParsable> : IValueConverter where TParsable : IParsable<TParsable>?
{
    [return: NotNullIfNotNull(nameof(value))]
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return string.Empty;

        if (value is not TParsable parsable)
            throw new InvalidOperationException($"Value should be of type {typeof(TParsable).Name}");

        return SystemConvert.ToString(parsable, CultureInfo.InvariantCulture)!;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null;

        var @string = value.ToString();

        if (TParsable.TryParse(@string, culture, out var result))
        {
            return result;
        }
        else if (TParsable.TryParse(@string, CultureInfo.InvariantCulture, out result))
        {
            return result;
        }

        return null;
    }
}
