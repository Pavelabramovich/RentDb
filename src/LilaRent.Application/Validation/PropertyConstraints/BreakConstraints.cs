namespace LilaRent.Application.Validation;


public static class BreakConstraints
{
    public const int MinValue = 0;
    public const string MinValueError = "{PropertyName} must be positive";

    public const int MaxValue = 720;
    public const string MaxValueError = "maximum {PropertyName} is 720 minutes";
}
