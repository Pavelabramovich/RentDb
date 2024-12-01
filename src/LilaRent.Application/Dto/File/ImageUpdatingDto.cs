namespace LilaRent.Application.Dto;


public record ImageUpdatingDto
{
    public required string ImageIdentifier { get; init; }
    public required int NewIndex { get; init; }
}
