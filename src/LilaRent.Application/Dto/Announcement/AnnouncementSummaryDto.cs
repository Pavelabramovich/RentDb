using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public record AnnouncementSummaryDto
{
    public required Guid Id { get; init; }

    public required Guid ProfileId { get; init; }
    public required string ProfileName { get; init; }

    public required string RentObjectName { get; init; }
    public required string Address { get; init; }
    public required string Description { get; init; }

    public required Price Price { get; init; }
    public required bool IsPromoted { get; init; }

    public required IReadOnlyList<string> Images { get; init; }
}
