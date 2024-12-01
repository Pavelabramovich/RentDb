namespace LilaRent.Application.Validation;


public static class AreaConstraints
{
    public const decimal MinValue = 0;
    public const string MinValueError = "{PropertyName} must be positive";

    public const decimal MaxValue = 99999;
    public const string MaxValueError = "maximum {PropertyName} value is 99999";
}
