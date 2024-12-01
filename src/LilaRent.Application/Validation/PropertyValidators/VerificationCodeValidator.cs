using FluentValidation;
using LilaRent.Application.Extensions;


namespace LilaRent.Application.Validation;


public class VerificationCodeValidator : AbstractValidator<string> 
{
    public VerificationCodeValidator(string? propertyName = null)
    {
        RuleFor(verificationCode => verificationCode)
            .Length(VerificationCodeConstraints.Length).WithMessage(VerificationCodeConstraints.LengthError)
            .SymbolGroupsMust(options =>
            {
                options.Symbols
                    .Must(VerificationCodeConstraints.IsCorrectSymbol)
                    .WithMessage(VerificationCodeConstraints.SymbolMismatchError);
            })
            .OverridePropertyName(propertyName ?? "verification code");
    }
}
