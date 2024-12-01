using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetSavedRefreshTokedAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default);
    Task UpsertUserRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task DeleteUserRefreshTokenAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default);
}
