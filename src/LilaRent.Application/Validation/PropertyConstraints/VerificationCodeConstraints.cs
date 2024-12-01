namespace LilaRent.Application.Validation;


public static class VerificationCodeConstraints
{
    public const int Length = 6;
    public const string LengthError = "allowed {PropertyName} length is 6";

    public static bool IsCorrectSymbol(char symbol) => char.IsBetween(symbol, '0', '9');
    public const string SymbolMismatchError = "{PropertyName} allows numbers from 0 to 9 only";
}
