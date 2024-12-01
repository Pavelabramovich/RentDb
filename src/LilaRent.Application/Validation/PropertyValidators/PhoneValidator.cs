using FluentValidation;


namespace LilaRent.Application.Validation;


public class PhoneValidator : AbstractValidator<string> 
{
    public PhoneValidator(string? propertyName = null)
    {
        RuleFor(phone => phone)
            .Must(phone => PhoneConstraints.IsValidPhoneFormat(phone))
            .WithMessage(PhoneConstraints.InvalidPhoneFormatError)
            .OverridePropertyName(propertyName ?? "phone");
    }
}
