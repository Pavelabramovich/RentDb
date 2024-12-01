using FluentValidation;
using FluentValidation.Results;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class EmailValidator : AbstractValidator<string> 
{
    public EmailValidator(string? propertyName = null)
    {
        RuleFor(email => email)
            .MinimumLength(EmailConstraints.MinLength).WithMessage(EmailConstraints.MinLengthError)
            .MaximumLength(EmailConstraints.MaxLength).WithMessage(EmailConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(EmailConstraints.IsCorrectLetter)
                    .WithMessage(EmailConstraints.LetterMismatchError);

                options.Digits
                    .Must(EmailConstraints.IsCorrectDigit)
                    .WithMessage(EmailConstraints.DigitMismatchError);

                options.Spaces
                    .Must(EmailConstraints.IsCorrectSpace)
                    .WithMessage(EmailConstraints.SpacesAreNotAllowedError);

                options.SpecialSymbols
                    .Must(EmailConstraints.IsCorrectSpecialSymbol)
                    .WithMessage(EmailConstraints.SpecialSymbolMismatchError);
            })
            .Must(email => EmailConstraints.IsCorrectEmailFormat(email)).WithMessage(EmailConstraints.InvalidEmailFormatError)
            .OverridePropertyName(propertyName ?? "email");
    }
}
