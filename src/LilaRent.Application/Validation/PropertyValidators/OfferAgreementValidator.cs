using FluentValidation;
using LilaRent.Application.Dto;
using System.Linq;


namespace LilaRent.Application.Validation;


public class OfferAgreementValidator : AbstractValidator<FileCreatingDto>
{
    public OfferAgreementValidator(string? propertyName = null)
    {
        RuleFor(offer => offer.Format)
            .Must(format => OfferAgreementConstraints.AllowedFormats.Contains(format))
            .WithMessage(OfferAgreementConstraints.IncorrectFormatError)
            .OverridePropertyName(propertyName ?? "offer agreement");

        //RuleFor(image => image.SizeInBytes)
        //    .LessThanOrEqualTo(OfferAgreementConstraints.MaxFileSizeInBytes)
        //    .WithMessage(OfferAgreementConstraints.MaxFileSizeError)
        //    .OverridePropertyName(propertyName ?? "offer agreement");
    }
}
