using FluentValidation;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class DescriptionValidator : AbstractValidator<string> 
{
    public DescriptionValidator(string? propertyName = null)
    {
        RuleFor(description => description)
            .MinimumLength(DescriptionConstraints.MinLength).WithMessage(DescriptionConstraints.MinLengthError)
            .MaximumLength(DescriptionConstraints.MaxLength).WithMessage(DescriptionConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(DescriptionConstraints.IsCorrectLetter)
                    .WithMessage(DescriptionConstraints.LetterMismatchError);

                options.Digits
                    .Must(DescriptionConstraints.IsCorrectDigit)
                    .WithMessage(DescriptionConstraints.DigitMismatchError);

                options.SpecialSymbols
                    .Must(DescriptionConstraints.IsCorrectSpecialSymbol)
                    .WithMessage(DescriptionConstraints.SpecialSymbolMismatchError);
            })
            .OverridePropertyName(propertyName ?? "description");
    }
}
