namespace LilaRent.Application.Validation;


public static class PercentageConstraints
{
    public const int MinValue = 0;
    public const string MinValueError = "{PropertyName} must be positive";

    public const int MaxValue = 100;
    public const string MaxValueError = "maximum value of {PropertyName} is 100%";
}
