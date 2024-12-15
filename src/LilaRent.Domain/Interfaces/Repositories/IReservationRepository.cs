using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface IReservationRepository : IRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetAnnouncementReservations(Guid announcementId);
}
