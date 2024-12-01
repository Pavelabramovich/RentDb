namespace LilaRent.Application.Dto;


public class TokensDto
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
