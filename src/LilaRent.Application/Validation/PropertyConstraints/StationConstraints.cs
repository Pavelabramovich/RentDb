using System.Text.RegularExpressions;


namespace LilaRent.Application.Validation;


public static partial class StationConstraints
{
    public const int MinLength = 3;
    public const string MinLengthError = "minimum {PropertyName} length is 3";

    public const int MaxLength = 70;
    public const string MaxLengthError = "maximum {PropertyName} length is 70";


    public static bool IsCorrectLetter(char letter) => CorrectLetterRegex().IsMatch(letter.ToString());
    public const string LetterMismatchError = "{PropertyName} allows letters of Russian and English alphabets only";


    [GeneratedRegex(@"[A-Za-zА-ЯЁа-яё]")]
    private static partial Regex CorrectLetterRegex();
}
