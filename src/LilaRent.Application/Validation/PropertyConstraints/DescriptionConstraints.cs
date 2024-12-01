using System.Text.RegularExpressions;


namespace LilaRent.Application.Validation;


public static partial class DescriptionConstraints
{
    public const int MinLength = 2;
    public const string MinLengthError = "minimum {PropertyName} length is 2";

    public const int MaxLength = 500;
    public const string MaxLengthError = "maximum {PropertyName} length is 500";


    public static bool IsCorrectLetter(char letter) => CorrectLetterRegex().IsMatch(letter.ToString());
    public const string LetterMismatchError = "{PropertyName} allows letters of Russian and English alphabets only";

    public static bool IsCorrectDigit(char digit) => char.IsBetween(digit, '0', '9');
    public const string DigitMismatchError = "{PropertyName} allows numbers from 0 to 9 only";

    public static bool IsCorrectSpecialSymbol(char specialSymbol) => s_correctSpecialSymbols.Contains(specialSymbol);
    public const string SpecialSymbolMismatchError = "allowed special characters in {PropertyName}: '.-#@%&/\",:?!$";


    [GeneratedRegex(@"[A-Za-zА-ЯЁа-яё]")]
    private static partial Regex CorrectLetterRegex();

    private static readonly HashSet<char> s_correctSpecialSymbols = new("'.-#@%&/\",:?!$");
}
