using FluentValidation;


namespace LilaRent.Application.Validation;


public class PercentageValidator : AbstractValidator<int>
{
    public PercentageValidator(string? propertyName = null)
    {
        RuleFor(percentage => percentage)
            .GreaterThanOrEqualTo(PercentageConstraints.MinValue).WithMessage(PercentageConstraints.MinValueError)
            .LessThanOrEqualTo(PercentageConstraints.MaxValue).WithMessage(PercentageConstraints.MaxValueError)
            .OverridePropertyName(propertyName ?? "percentage");
    }
}
