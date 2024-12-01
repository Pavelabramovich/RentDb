using FluentValidation;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class StationValidator : AbstractValidator<string>
{
    public StationValidator(string? propertyName = null)
    {
        RuleFor(station => station)
            .MinimumLength(StationConstraints.MinLength).WithMessage(StationConstraints.MinLengthError)
            .MaximumLength(StationConstraints.MaxLength).WithMessage(StationConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(StationConstraints.IsCorrectLetter)
                    .WithMessage(StationConstraints.LetterMismatchError);
            })
            .OverridePropertyName(propertyName ?? "station");
    }
}
