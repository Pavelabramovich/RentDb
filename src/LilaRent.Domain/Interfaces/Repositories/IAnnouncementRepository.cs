using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface IAnnouncementRepository : IRepository<Announcement>
{
    Task<Announcement?> GetByIdWithImagesAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateWithImagesAsync(Guid id, Announcement entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<Announcement>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Announcement>> GetAllWithImagesAsync(CancellationToken cancellationToken = default);
}
