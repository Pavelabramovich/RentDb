using LilaRent.Domain.Entities;

namespace LilaRent.Application.Dto;


public record LegalPersonProfileDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Image { get; init; }

    public string? Address { get; init; } = null;
    public int? Floor { get; init; } = null;
    public string? Station { get; init; } = null;
    public decimal? Area { get; init; } = null;

    public required IEnumerable<AnnouncementSummaryDto> Announcements { get; init; }
}

