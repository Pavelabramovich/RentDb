using MediatR;
using System.Security.Claims;
using LilaRent.Application.Dto;
using LilaRent.Domain.Interfaces;
using LilaRent.Application.Services;
using LilaRent.Domain.Entities;


namespace LilaRent.Application.UseCases.Commands;


internal class RefreshHandler : IRequestHandler<RefreshCommand, TokensDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenService _jwtTokenService;


    public RefreshHandler(IUnitOfWork unitOfWork, JwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<TokensDto> Handle(RefreshCommand request, CancellationToken cancellationToken = default)
    {
        var accessToken = request.TokensDto.AccessToken;
        var refreshToken = request.TokensDto.RefreshToken;

        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(accessToken);


        var claims = principal.Claims;

        var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!;
        var userId = Guid.Parse(userIdClaim.Value);

        var savedRefreshToken = await _unitOfWork.RefreshTokenRepository
            .GetSavedRefreshTokedAsync(userId, refreshToken, cancellationToken);

        if (savedRefreshToken?.Value != refreshToken)
        {
            throw new UnauthorizedAccessException();
        }

        var newTokens = _jwtTokenService.GenerateRefreshToken(userId, claims.ToArray()) 
            ?? throw new UnauthorizedAccessException();

        var newRefreshToken = new RefreshToken
        {
            UserId = userId,
            Value = newTokens.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(30)
        };

        await _unitOfWork.RefreshTokenRepository.UpsertUserRefreshTokenAsync(newRefreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newTokens;
    }
}
