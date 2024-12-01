using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;


namespace LilaRent.Application.Mapping;


public partial class Mapper : AutoMapper.Profile
{
    private void CreateAnnouncementMaps()
    {
        CreateMap<(AnnouncementCreatingDto Dto, IEnumerable<string> Images, string OfferAgreement), Announcement>()
            .ConstructUsing(src => new Announcement
            {
                ProfileId = src.Dto.ProfileId,
                RentObjectName = src.Dto.RentObjectName,
                Address = src.Dto.Address,
                Description = src.Dto.Description,
                Price = src.Dto.Price,
                IsPromoted = src.Dto.IsPromoted,
                OpenTime = src.Dto.OpenTime,
                CloseTime = src.Dto.CloseTime,
                BreakBetweenReservations = src.Dto.BreakBetweenReservations,
                MinReservationTime = src.Dto.MinReservationTime,
                MaxReservationTime = src.Dto.MaxReservationTime,
                CanClientsChangeRecords = src.Dto.CanClientsChangeRecords,
                CanClientsDisableRecords = src.Dto.CanClientsDisableRecords,
                UseDiscount = src.Dto.UseDiscount,
                MinTimeForDiscount = src.Dto.MinTimeForDiscount,
                MaxTimeForDiscount = src.Dto.MaxTimeForDiscount,
                DiscountPercentage = src.Dto.DiscountPercentage,
                Images = src.Images.Select((image, index) => new AnnouncementImage() 
                { 
                    AnnouncementId = default, 
                    ImagePath = image, 
                    Index = index 
                })
                .ToList(),
                OfferAgreementPath = src.OfferAgreement
            });
    }
}
