namespace LilaRent.Application.Dto;


public record UserWithProfileCreatingDto
{
    public required string Login { get; init; }
    public required string Password { get; init; }

    public required ProfileCreatingDto Profile { get; init; }
}
