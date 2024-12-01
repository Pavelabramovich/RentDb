using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public record ProfileSummaryDto
{
    public required Guid Id { get; init; }
    public required LegalEntityType LegalEntityType { get; init; }
    public required string Name { get; init; }
    public required string ImageUri { get; init; }
}
