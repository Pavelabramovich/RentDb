using FluentValidation;


namespace LilaRent.Application.Validation;


public class DaysCountOpenAheadValidator : AbstractValidator<int>
{
    public DaysCountOpenAheadValidator(string? propertyName = null)
    {
        RuleFor(daysCountOpenAhead => daysCountOpenAhead)
            .GreaterThanOrEqualTo(DaysCountOpenAheadConstraints.MinValue).WithMessage(DaysCountOpenAheadConstraints.MinValueError)
            .LessThanOrEqualTo(DaysCountOpenAheadConstraints.MaxValue).WithMessage(DaysCountOpenAheadConstraints.MaxValueError)
            .OverridePropertyName(propertyName ?? "days count open ahead");
    }
}
