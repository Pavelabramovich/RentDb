namespace LilaRent.Application.Dto;


public record FileDto
{
    public required string Identifier { get; init; }
    public required string Uri { get; init; }
}
