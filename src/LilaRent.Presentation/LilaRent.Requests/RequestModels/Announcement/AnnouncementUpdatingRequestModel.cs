using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using Microsoft.AspNetCore.Http;


namespace LilaRent.Requests.RequestModels;


public record AnnouncementUpdatingRequestModel()
{
    public required Guid Id { get; init; }

    public required string RentObjectName { get; init; }
    public required string Address { get; init; }
    public required string Description { get; init; }

    public required Price Price { get; init; }
    public required bool IsPromoted { get; init; }

    public required TimeOnly OpenTime { get; init; }
    public required TimeOnly CloseTime { get; init; }
    public required TimeSpan BreakBetweenReservations { get; init; }

    public TimeSpan? MinReservationTime { get; init; }
    public TimeSpan? MaxReservationTime { get; init; }

    public required bool CanClientsChangeRecords { get; init; }
    public required bool CanClientsDisableRecords { get; init; }

    public required bool UseDiscount { get; init; }
    public TimeSpan? MinTimeForDiscount { get; init; }
    public TimeSpan? MaxTimeForDiscount { get; init; }
    public int? DiscountPercentage { get; init; }

    public IEnumerable<ImageCreatingRequestModel>? NewImages { get; init; }
    public IEnumerable<ImageUpdatingDto>? UpdatedImages { get; init; }
    public IEnumerable<string>? DeletedImages { get; init; }

    public IFormFile? NewOfferAgreement { get; init; }
}
