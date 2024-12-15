using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;

namespace LilaRent.Application.Tests.Fakes;


internal class FakeReservationRepository : FakeRepository<Reservation>, IReservationRepository
{
    public FakeReservationRepository(ICollection<Reservation> reservations)
        : base(reservations)
    { }

    public Task<IEnumerable<Reservation>> GetAnnouncementReservations(Guid announcementId)
    {
        throw new NotImplementedException();
    }

    protected override object GetId(Reservation entity)
    {
        return entity.Id;
    }
}
