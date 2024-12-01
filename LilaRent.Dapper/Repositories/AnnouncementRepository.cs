using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly string _connectionString;


    public AnnouncementRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw null;
    }


    public Task AddAsync(Announcement entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Announcement?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> GetAllWithImagesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Announcement?> GetByIdWithImagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> GetWhereAsync(Expression<Func<Announcement, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, Announcement entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWithImagesAsync(Guid id, Announcement entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
