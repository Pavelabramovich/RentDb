using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeUserRepository : FakeRepository<User>, IUserRepository
{
    public FakeUserRepository(ICollection<User> users)
        : base(users)
    { }


    protected override object GetId(User entity)
    {
        return entity.Id;   
    }

    public Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAuthenticatedAsync(string login, string hashedPassword, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
