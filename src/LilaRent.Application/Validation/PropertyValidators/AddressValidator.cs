using FluentValidation;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class AddressValidator : AbstractValidator<string>
{
    public AddressValidator(string? propertyName = null)
    {
        RuleFor(address => address)
            .MinimumLength(AddressConstraints.MinLength).WithMessage(AddressConstraints.MinLengthError)
            .MaximumLength(AddressConstraints.MaxLength).WithMessage(AddressConstraints.MaxLengthError)
            .SymbolGroupsMust(options =>
            {
                options.Letters
                    .Must(AddressConstraints.IsCorrectLetter)
                    .WithMessage(AddressConstraints.LetterMismatchError);
            })
            .OverridePropertyName(propertyName ?? "address");

    }
}
