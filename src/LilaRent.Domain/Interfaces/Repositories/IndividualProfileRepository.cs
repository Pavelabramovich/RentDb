using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface IIndividualProfileRepository : IRepository<IndividualProfile>
{
    Task<IEnumerable<Announcement>> GetPrevioslyReservedAnnouncements(Guid individualId);
    Task<IEnumerable<Reservation>> GetReservations(Guid profileId);
}
