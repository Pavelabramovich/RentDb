using FluentValidation;
using LilaRent.Application.Dto;


namespace LilaRent.Application.Validation;


public class UserWithProfileCreatingDtoValidator : AbstractValidator<UserWithProfileCreatingDto>
{
    public UserWithProfileCreatingDtoValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty().WithMessage("Login is required")
            .SetValidator(new EmailValidator("login"));

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required")
            .SetValidator(new PasswordValidator(propertyName: "password"));

        RuleFor(u => u.Profile)
            .SetValidator(new ProfileCreatingDtoValidator());
    }
}
