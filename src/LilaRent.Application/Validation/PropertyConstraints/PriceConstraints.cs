namespace LilaRent.Application.Validation;


public static class PriceConstraints
{
    public const decimal MinValue = 0;
    public const string MinValueError = "{PropertyName} must be positive";
}
