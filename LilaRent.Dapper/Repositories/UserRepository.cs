using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Npgsql;
using System.Data;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class UserRepository : IUserRepository
{
    private readonly string _connectionString;


    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public async Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        await connection.ExecuteAsync(UserSql.Add, entity);
    }

    public Task<User?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<User>(UserSql.GetByLogin, new { Login = login });

        return result.FirstOrDefault(); 
    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAuthenticatedAsync(string login, string hashedPassword, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, User entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
