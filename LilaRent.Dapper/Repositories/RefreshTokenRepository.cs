using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


internal class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly string _connectionString;


    public RefreshTokenRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw null;
    }



    public Task AddAsync(RefreshToken entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserRefreshTokenAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<RefreshToken>(RefreshTokenQueries.All);
            return result.ToList();
        }
    }

    public Task<RefreshToken?> GetSavedRefreshTokedAsync(Guid userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RefreshToken>> GetWhereAsync(Expression<Func<RefreshToken, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RefreshToken>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, RefreshToken entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpsertUserRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
