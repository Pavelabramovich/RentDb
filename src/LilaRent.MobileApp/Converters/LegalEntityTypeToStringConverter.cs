using LilaRent.Domain.Fields;
using System.Globalization;


namespace LilaRent.MobileApp.Converters;


public class LegalEntityTypeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            LegalEntityType.LegalPerson => "Legal person",
            LegalEntityType.Individual => "Individual",

            _ => throw new ArgumentException("Unknown type of legal person.")
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            "legal person" => LegalEntityType.LegalPerson,
            "individual" => LegalEntityType.Individual,

            _ => throw new ArgumentException("Unknown type of legal person.")
        };
    }
}
