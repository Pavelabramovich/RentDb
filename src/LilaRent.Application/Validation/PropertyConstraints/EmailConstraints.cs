using System.Text.RegularExpressions;


namespace LilaRent.Application.Validation;


public static partial class EmailConstraints
{
    public const int MinLength = 6;
    public const string MinLengthError = "minimum {PropertyName} length is 6";

    public const int MaxLength = 30;
    public const string MaxLengthError = "maximum {PropertyName} length is 30";


    public static bool IsCorrectLetter(char letter) => CorrectLetterRegex().IsMatch(letter.ToString());
    public const string LetterMismatchError = "{PropertyName} allows letters of Russian and English alphabets only";

    public static bool IsCorrectDigit(char digit) => char.IsBetween(digit, '0', '9');
    public const string DigitMismatchError = "{PropertyName} allows numbers from 0 to 9 only";

    public static bool IsCorrectSpace(char space) => !char.IsWhiteSpace(space);
    public const string SpacesAreNotAllowedError = "spaces in {PropertyName} are not allowed";

    public static bool IsCorrectSpecialSymbol(char specialSymbol) => s_correctSpecialSymbols.Contains(specialSymbol);
    public const string SpecialSymbolMismatchError = "allowed special characters in {PropertyName}: '.-#@%&/";


    public static bool IsCorrectEmailFormat(string login) => CorrectEmailRegex().IsMatch(login);
    public const string InvalidEmailFormatError = "invalid email format in {PropertyName}"; // mb more information for user?


    [GeneratedRegex(@"[A-Za-zА-ЯЁа-яё]")]
    private static partial Regex CorrectLetterRegex();

    private static readonly HashSet<char> s_correctSpecialSymbols = new("'.-#@%&/\"");

    [GeneratedRegex(@"^[^@]+@[^@]+\.[^@]{2,}$")]
    private static partial Regex CorrectEmailRegex();
}
