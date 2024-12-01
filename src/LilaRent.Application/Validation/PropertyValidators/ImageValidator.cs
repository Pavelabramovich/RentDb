using FluentValidation;
using LilaRent.Application.Dto;


namespace LilaRent.Application.Validation;


public class ImageValidator : AbstractValidator<FileCreatingDto>
{
    public ImageValidator(string? propertyName = null)
    {
        RuleFor(image => image.Format)
            .Must(format => ImageConstraints.AllowedFormats.Contains(format))
            .WithMessage(ImageConstraints.IncorrectFormatError)
            .OverridePropertyName(propertyName ?? "image");

        //RuleFor(image => image.SizeInBytes)
        //    .LessThanOrEqualTo(ImageConstraints.MaxFileSizeInBytes)
        //    .WithMessage(ImageConstraints.MaxFileSizeError)
        //    .OverridePropertyName(propertyName ?? "image");
    }
}
