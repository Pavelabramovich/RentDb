using System.Text.RegularExpressions;


namespace LilaRent.Application.Validation;


public static partial class PhoneConstraints
{
    public static bool IsValidPhoneFormat(string phone)
    {
        return CorrectBelorussianPhoneRegex().IsMatch(phone) || CorrectRussianPhoneRegex().IsMatch(phone);
    }
    public const string InvalidPhoneFormatError = "allowed {PropertyName} formats are only +375 (XX) XXX XXXX and +7 (ХХХ) ХХХ ХХХХ";


    [GeneratedRegex(@"^\+375 \(\d{2}\) \d{3} \d{4}$")]
    private static partial Regex CorrectBelorussianPhoneRegex();

    [GeneratedRegex(@"^\+7 \(\d{3}\) \d{3} \d{4}$")]
    private static partial Regex CorrectRussianPhoneRegex();
}
