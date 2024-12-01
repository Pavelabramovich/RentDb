using FluentValidation;


namespace LilaRent.Application.Validation;


public class AreaValidator : AbstractValidator<decimal>
{
    public AreaValidator(string? propertyName = null)
    {
        RuleFor(area => area)
            .GreaterThanOrEqualTo(AreaConstraints.MinValue).WithMessage(AreaConstraints.MinValueError)
            .LessThanOrEqualTo(AreaConstraints.MaxValue).WithMessage(AreaConstraints.MaxValueError)
            .OverridePropertyName(propertyName ?? "area");
    }
}
