namespace LilaRent.Application.Dto;


public record IndividualProfileDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    public required string Phone { get; init; }
}
