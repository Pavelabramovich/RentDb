using FluentValidation;
using LilaRent.Application.Dto;


namespace LilaRent.Application.Validation;


public class ProfileCreatingDtoValidator : AbstractValidator<ProfileCreatingDto>
{
    public ProfileCreatingDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Profile name is required")
            .SetValidator(new NameValidator("name"));

        RuleFor(p => p.Phone)
            .NotEmpty().WithMessage("Profile phone is required")
            .SetValidator(new PhoneValidator("phone"));

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Profile email is required")
            .SetValidator(new EmailValidator("email"));

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Profile description is required")
            .SetValidator(new DescriptionValidator("description"));

        RuleFor(p => p.Image)
            .SetValidator(new ImageValidator("image"));
    }
}
