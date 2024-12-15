using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeAnnouncementsRepository : FakeRepository<Announcement>, IAnnouncementRepository
{
    public FakeAnnouncementsRepository(ICollection<Announcement> announcements)
        : base(announcements)
    { }

    protected override object GetId(Announcement entity) => entity.Id;  


    public Task<IEnumerable<Announcement>> GetAllWithImagesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.AsEnumerable());
    }

    public Task<Announcement?> GetByIdWithImagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.FirstOrDefault(e => e.Id == id));
    }

    public Task<IEnumerable<Announcement>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWithImagesAsync(Guid id, Announcement entity, CancellationToken cancellationToken = default)
    {
        return UpdateAsync(id, entity, cancellationToken);
    }
}
