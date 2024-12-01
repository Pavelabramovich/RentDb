using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Application.Exceptions;
using LilaRent.Application.Services;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using LileRent.Application.UseCases.Commands;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


internal class LoginHandler : IRequestHandler<LoginCommand, TokensDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CryptographyService _cryptographyService;
    private readonly JwtTokenService _jwtTokenService;


    public LoginHandler(
        IUnitOfWork unitOfWork,
        CryptographyService cryptographyService,
        JwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _cryptographyService = cryptographyService;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var credentialsDto = request.CredentialsDto;

        var login = credentialsDto.Login;
        var password = credentialsDto.Password;

       
        var user = await _unitOfWork.UserRepository.FindByLoginAsync(login, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException(login, $"User with login = {login} does not exist");
        }

        if (!AreCredentialsValid(password, user))
        {
            throw new EntityNotFoundException((login, password), "Invalid user credentials");
        }


        //var userRoles = await _unitOfWork.UserRepository.GetUserRolesAsync(user!.Id, cancellationToken);
        //var userClaims = await _unitOfWork.ClaimRepository.GetUserClaimsAsync(user!.Id, cancellationToken);

        //var claimsFromRoles = userRoles.Select(r => new SystemClaim(ClaimTypes.Role, r.Name));
        //var claimsFromClaims = userClaims.Select(c => new SystemClaim(c.Type, c.Value));

        //SystemClaim[] claims = [.. claimsFromRoles, .. claimsFromClaims];

        var tokens = _jwtTokenService.GenerateToken(user.Id) 
            ?? throw new UnauthorizedAccessException();

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Value = tokens.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(30)
        };

        await _unitOfWork.RefreshTokenRepository.UpsertUserRefreshTokenAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tokens;
    }


    private bool AreCredentialsValid(string testPassword, User user)
    {
        var hash = _cryptographyService.HashPassword(testPassword, user.Salt);
        return hash == user.HashedPassword;
    }
}
