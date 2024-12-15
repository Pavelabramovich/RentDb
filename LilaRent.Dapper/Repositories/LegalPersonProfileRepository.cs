using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Npgsql;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class LegalPersonProfileRepository : ILegalPersonProfileRepository
{
    private readonly string _connectionString;


    public LegalPersonProfileRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public Task AddAsync(LegalPersonProfile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<LegalPersonProfile?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var legalPersons = await connection.QueryAsync<LegalPersonProfile>(LegalPersonProfileSql.GetById, new { Id = (Guid)id });

        return legalPersons.FirstOrDefault();
    }

    public Task<IEnumerable<LegalPersonProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetReservations(Guid profileId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LegalPersonProfile>> GetWhereAsync(Expression<Func<LegalPersonProfile, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LegalPersonProfile>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, LegalPersonProfile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
