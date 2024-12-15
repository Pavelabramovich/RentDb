using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeProfileRepository : FakeRepository<Profile>, IProfileRepository
{
    public FakeProfileRepository(ICollection<Profile> profiles)
        : base(profiles)
    { }

    protected override object GetId(Profile entity)
    {
        return entity.Id;
    }
}
