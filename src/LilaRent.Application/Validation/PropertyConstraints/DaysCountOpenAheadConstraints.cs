namespace LilaRent.Application.Validation;


public static class DaysCountOpenAheadConstraints
{
    public const int MinValue = 1;
    public const string MinValueError = "minimum {PropertyName} value is 1";

    public const int MaxValue = 365;
    public const string MaxValueError = "maximum {PropertyName} value is 365";
}
