namespace LilaRent.Application.Validation;


public static partial class PasswordConstraints
{
    public const int MinLength = 6;
    public const string MinLengthError = "minimum {PropertyName} length is 6";

    public const int MaxLength = 20;
    public const string MaxLengthError = "maximum {PropertyName} length is 20";

    public static bool IsCorrectUpper(char letter) => char.IsBetween(letter, 'A', 'Z') || char.IsBetween(letter, 'А', 'Я') || letter == 'Ё';
    public static bool IsCorrectLower(char letter) => char.IsBetween(letter, 'a', 'z') || char.IsBetween(letter, 'а', 'я') || letter == 'ё';
    public static bool IsCorrectLetter(char letter) => IsCorrectUpper(letter) || IsCorrectLower(letter);
    public const string LetterMismatchError = "{PropertyName} allows letters of English alphabets only";

    public static bool IsCorrectDigit(char digit) => char.IsBetween(digit, '0', '9'); 
    public const string DigitMismatchError = "{PropertyName} allows numbers from 0 to 9 only";

    public static bool IsCorrectSpace(char space) => !char.IsWhiteSpace(space);
    public const string SpacesAreNotAllowedError = "spaces in {PropertyName} are not allowed";

    public static bool IsCorrectSpecialSymbol(char specialSymbol) => s_correctSpecialSymbols.Contains(specialSymbol);
    public const string SpecialSymbolMismatchError = "{PropertyName} allows special characters: !#$%&'*+,-./:;<=>?@^_`|~()[]{}";


    private static readonly HashSet<char> s_correctSpecialSymbols = "!#$%&'()*+,\\-./:;<=>?@[\\]^_`{|}~".ToHashSet();
}
