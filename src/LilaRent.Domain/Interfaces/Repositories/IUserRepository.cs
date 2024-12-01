using LilaRent.Domain.Entities;


namespace LilaRent.Domain.Interfaces;


public interface IUserRepository : IRepository<User>
{
    Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task<bool> IsAuthenticatedAsync(string login, string hashedPassword, CancellationToken cancellationToken = default);
}
