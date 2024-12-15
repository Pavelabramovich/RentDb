using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface ILegalPersonProfileRepository : IRepository<LegalPersonProfile>
{
    Task<IEnumerable<Reservation>> GetReservations(Guid profileId);
}
