using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;

namespace LilaRent.Application.Tests.Fakes;


internal class FakeLegalPersonProfileRepository : FakeRepository<LegalPersonProfile>, ILegalPersonProfileRepository
{
    public FakeLegalPersonProfileRepository(ICollection<LegalPersonProfile> legalPersonProfiles)
        : base(legalPersonProfiles)
    { }

    public Task<IEnumerable<Reservation>> GetReservations(Guid profileId)
    {
        throw new NotImplementedException();
    }

    protected override object GetId(LegalPersonProfile entity)
    {
        return entity.Id;
    }
}
