namespace LilaRent.MobileApp.Extensions;


public static class ToFirstUpperRestLowerExtension
{
    public static string ToFirstUpperRestLower(this string @string)
    {
        if (string.IsNullOrEmpty(@string)) 
            return @string;

        char first = char.ToUpper(@string[0]);
        string rest = @string[1..].ToLowerInvariant();

        return $"{first}{rest}";
    }
}
