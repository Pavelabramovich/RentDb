namespace LilaRent.Application.Dto;


public record CredentialsDto
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}
