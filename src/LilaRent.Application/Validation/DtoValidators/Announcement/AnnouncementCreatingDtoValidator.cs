using FluentValidation;
using LilaRent.Application.Dto;


namespace LilaRent.Application.Validation;


public class AnnouncementCreatingDtoValidator : AbstractValidator<AnnouncementCreatingDto>
{
    public AnnouncementCreatingDtoValidator()
    {
        RuleFor(a => a.RentObjectName)
            .NotEmpty().WithMessage("rent object name is required")
            .SetValidator(new NameValidator("rent object name"));

        RuleFor(a => a.Description)
            .NotEmpty().WithMessage("description is required")
            .SetValidator(new DescriptionValidator("description"));

        //RuleFor(a => a.Price)
        //    .SetValidator(new PriceValidator("price"));

        RuleFor(a => a.CloseTime)
            .GreaterThanOrEqualTo(a => a.OpenTime).WithMessage("close time should be greater then open time");

        RuleFor(a => a.MaxReservationTime)
            .GreaterThanOrEqualTo(a => a.MinReservationTime).WithMessage("minimum reservation time must be geater then minimum reservation time")
            .When(a => a.MinReservationTime is not null && a.MaxReservationTime is not null);

        //RuleFor(a => a.Images)
        //    .NotEmpty().WithMessage("at least one image required")
        //    .Must(images => images.Count() <= 8).WithMessage("Max count of images is 8")
        //    .ForEach(imageRule => imageRule.SetValidator(new ImageValidator("image")));

        //RuleFor(a => a.OfferAgreement)
        //    .SetValidator(new OfferAgreementValidator("offer agreement"));

        RuleFor(a => a.MaxTimeForDiscount)
            .GreaterThanOrEqualTo(a => a.MaxTimeForDiscount)
            .When(a => a.MaxReservationTime is not null);

        RuleFor(a => a.MinTimeForDiscount)
            .NotEmpty().WithMessage("minimum reservation time for discount is required")
            .When(a => a.UseDiscount);

        RuleFor(a => a.DiscountPercentage)
            .NotEmpty().WithMessage("discount percentage is requires")
            .When(a => a.UseDiscount);
    }
}
