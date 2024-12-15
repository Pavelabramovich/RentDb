using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Npgsql;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class ReservationRepository : IReservationRepository
{
    private readonly string _connectionString;


    public ReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public async Task AddAsync(Reservation entity, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        await connection.ExecuteAsync(ReservationSql.Add, entity);
    }

    public Task<Reservation?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Reservation>> GetAnnouncementReservations(Guid announcementId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var reservations = await connection.QueryAsync<Reservation>(ReservationSql.GetByAnnouncementId , new { AnnouncementId = announcementId });

        return reservations;
    }

    public Task<IEnumerable<Reservation>> GetWhereAsync(Expression<Func<Reservation, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, Reservation entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
