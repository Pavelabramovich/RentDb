using FluentValidation;
using FluentValidation.Validators;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator(string? propertyName = null)
    {
        RuleFor(password => password)
            .MinimumLength(PasswordConstraints.MinLength).WithMessage(PasswordConstraints.MinLengthError)
            .MaximumLength(PasswordConstraints.MaxLength).WithMessage(PasswordConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(PasswordConstraints.IsCorrectLetter)
                    .WithMessage(PasswordConstraints.LetterMismatchError);

                options.Digits
                    .Must(PasswordConstraints.IsCorrectDigit)
                    .WithMessage(PasswordConstraints.DigitMismatchError);

                options.Spaces
                    .Must(PasswordConstraints.IsCorrectSpace)
                    .WithMessage(PasswordConstraints.SpacesAreNotAllowedError);

                options.SpecialSymbols
                    .Must(PasswordConstraints.IsCorrectSpecialSymbol)
                    .WithMessage(PasswordConstraints.SpecialSymbolMismatchError);
            })
            .OverridePropertyName(propertyName ?? "password");
        }
}
