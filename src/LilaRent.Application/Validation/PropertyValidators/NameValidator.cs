using FluentValidation;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class NameValidator : AbstractValidator<string> 
{
    public NameValidator(string? propertyName = null)
    {
        RuleFor(organizationName => organizationName)
            .MinimumLength(NameConstraints.MinLength).WithMessage(NameConstraints.MinLengthError)
            .MaximumLength(NameConstraints.MaxLength).WithMessage(NameConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(NameConstraints.IsCorrectLetter)
                    .WithMessage(NameConstraints.LetterMismatchError);

                options.Digits
                    .Must(NameConstraints.IsCorrectDigit)
                    .WithMessage(NameConstraints.DigitMismatchError);

                options.SpecialSymbols
                    .Must(NameConstraints.IsCorrectSpecialSymbol)
                    .WithMessage(NameConstraints.SpecialSymbolMismatchError);
            })
            .OverridePropertyName(propertyName ?? "organization name");
    }
}
