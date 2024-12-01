using System.Linq.Expressions;


namespace LilaRent.Domain.Interfaces;


public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default);

    Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(object id, TEntity entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(object id, CancellationToken cancellationToken = default);
}
