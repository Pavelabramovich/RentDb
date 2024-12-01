namespace LilaRent.Application.Validation;


public static class FloorConstraints
{
    public const int MinValue = -99;
    public const string MinValueError = "minimum {PropertyName} is -99";

    public const int MaxValue = 999;
    public const string MaxValueError = "maximum {PropertyName} is 999";
}
