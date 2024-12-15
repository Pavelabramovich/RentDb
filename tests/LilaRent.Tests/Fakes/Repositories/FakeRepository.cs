using LilaRent.Domain.Interfaces;
using System.Linq.Expressions;


namespace LilaRent.Application.Tests.Fakes;


internal abstract class FakeRepository<T> : IRepository<T>
{
    protected readonly ICollection<T> _entities;


    public FakeRepository(ICollection<T> entities)
    {
        _entities = entities;
    }

    protected abstract object GetId(T entity);


    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entities.Add(entity);
        return Task.CompletedTask;
    }

    public Task<T?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.FirstOrDefault(e => GetId(e).Equals(id)));
    }

    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.AsEnumerable());
    }

    public Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        var predicate = filter.Compile();

        return Task.FromResult(_entities.Where(predicate));
    }

    public Task<IEnumerable<T>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.Skip(skip).Take(take));
    }

    public Task UpdateAsync(object id, T entity, CancellationToken cancellationToken = default)
    {
        var existed = _entities.FirstOrDefault(e => GetId(e).Equals(id))
            ?? throw null;

        _entities.Remove(existed);
        _entities.Add(entity);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        var existed = _entities.FirstOrDefault(e => GetId(e).Equals(id))
            ?? throw null;

        _entities.Remove(existed);

        return Task.CompletedTask;
    }
}
