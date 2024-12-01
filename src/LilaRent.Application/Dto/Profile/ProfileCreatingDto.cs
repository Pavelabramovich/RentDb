using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public record ProfileCreatingDto
{
    public required LegalEntityType LegalEntityType { get; init; }

    public required string Name { get; init; }
    public required string Phone { get; init; }
    public required string Email { get; init; }

    public required string Description { get; init; }
    public required FileCreatingDto Image { get; init; }
}
