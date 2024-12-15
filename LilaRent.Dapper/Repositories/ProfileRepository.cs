using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Npgsql;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class ProfileRepository : IProfileRepository
{
    private readonly string _connectionString;


    public ProfileRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public Task AddAsync(Profile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Profile?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        var profiles = await GetAllAsync(cancellationToken);

        return profiles.FirstOrDefault(p => p.Id == (Guid)id);
    }

    public async Task<IEnumerable<Profile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var legalPersons = await connection.QueryAsync<LegalPersonProfile>(ProfileSql.GetAllLegalPersons);
        var individuals = await connection.QueryAsync<IndividualProfile>(ProfileSql.GetAllIndividuals);

        return legalPersons.Cast<Profile>().Concat(individuals);
    }

    public Task<IEnumerable<Profile>> GetWhereAsync(Expression<Func<Profile, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Profile>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, Profile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
