using FluentValidation;
using LilaRent.Application.Dto;


namespace LilaRent.Application.Validation;


public class AuthenticationDtoValidator : AbstractValidator<CredentialsDto>
{
    public AuthenticationDtoValidator()
    {
        RuleFor(a => a.Login)
            .NotEmpty().WithMessage("Login must be not empty")
            .SetValidator(new EmailValidator(propertyName: "login"));

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("Password is required")
            .SetValidator(new PasswordValidator(propertyName: "password"));
    }
}
