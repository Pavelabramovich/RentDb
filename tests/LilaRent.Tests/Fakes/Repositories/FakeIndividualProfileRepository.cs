using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeIndividualProfileRepository : FakeRepository<IndividualProfile>, IIndividualProfileRepository
{
    public FakeIndividualProfileRepository(ICollection<IndividualProfile> individualProfiles)
        : base(individualProfiles)
    { }


    protected override object GetId(IndividualProfile entity)
    {
        return entity.Id;
    }


    public Task<IEnumerable<Announcement>> GetPrevioslyReservedAnnouncements(Guid individualId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetReservations(Guid profileId)
    {
        throw new NotImplementedException();
    }
}
