using System.Linq.Expressions;

namespace For_A_Donation.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();

    TEntity? GetById(Guid Id);

    IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(TEntity entity);

    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}
