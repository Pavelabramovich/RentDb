using FluentValidation;


namespace LilaRent.Application.Validation;


public class FloorValidator : AbstractValidator<int>
{
    public FloorValidator(string? propertyName = null)
    {
        RuleFor(floor => floor)
            .GreaterThanOrEqualTo(FloorConstraints.MinValue).WithMessage(FloorConstraints.MinValueError)
            .LessThanOrEqualTo(FloorConstraints.MaxValue).WithMessage(FloorConstraints.MaxValueError)
            .OverridePropertyName(propertyName ?? "floor");
    }
}
