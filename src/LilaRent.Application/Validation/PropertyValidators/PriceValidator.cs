using FluentValidation;


namespace LilaRent.Application.Validation;


public class PriceValidator : AbstractValidator<decimal>
{
    public PriceValidator(string? propertyName = null)
    {
        RuleFor(price => price)
            .GreaterThan(PriceConstraints.MinValue).WithMessage(PriceConstraints.MinValueError)
            .OverridePropertyName(propertyName ?? "price");
    }
}
