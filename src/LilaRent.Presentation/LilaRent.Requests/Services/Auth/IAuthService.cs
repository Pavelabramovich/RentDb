using LilaRent.Application.Dto;

namespace LilaRent.Requests.Services;


public interface IAuthService
{
    Task<TokensDto> LoginAsync(CredentialsDto credentials); 
    Task<TokensDto> RefreshAsync(TokensDto tokens);
}
