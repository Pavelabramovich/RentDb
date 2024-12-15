using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;

namespace LilaRent.Application.Tests.Fakes;


internal class FakeRefreshTokenRepository : FakeRepository<RefreshToken>, IRefreshTokenRepository
{
    public FakeRefreshTokenRepository(ICollection<RefreshToken> refreshTokens)
        : base(refreshTokens)
    { }


    protected override object GetId(RefreshToken entity)
    {
        return entity.UserId;
    }


    public Task DeleteUserRefreshTokenAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken?> GetSavedRefreshTokedAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpsertUserRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
