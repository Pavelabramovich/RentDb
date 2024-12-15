using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public record AnnouncementDetailsDto
{
    public required Guid Id { get; init; }
    public required Guid ProfileId { get; init; }
    public required string ProfileName { get; init; }

    public required string RentObjectName { get; init; }
    public required string Description { get; init; }

    public required Price Price { get; init; }
    public required bool IsPromoted { get; init; }

    public required IEnumerable<FileDto> Images { get; init; }
    public required FileDto OfferAgreement { get; init; }

    public required TimeOnly OpenTime { get; init; }
    public required TimeOnly CloseTime { get; init; }

    public required IEnumerable<AnnouncementSummaryDto> SimularAnnouncements { get; init; }
}
