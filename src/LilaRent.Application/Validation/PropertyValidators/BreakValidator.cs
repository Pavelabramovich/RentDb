using FluentValidation;


namespace LilaRent.Application.Validation;


public class BreakValidator : AbstractValidator<int>
{
    public BreakValidator(string? propertyName = null)
    {
        RuleFor(percentage => percentage)
            .GreaterThanOrEqualTo(BreakConstraints.MinValue).WithMessage(BreakConstraints.MinValueError)
            .LessThanOrEqualTo(BreakConstraints.MaxValue).WithMessage(BreakConstraints.MaxValueError)
            .OverridePropertyName(propertyName ?? "break");
    }
}
