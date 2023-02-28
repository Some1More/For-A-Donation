using System.Linq.Expressions;

namespace For_A_Donation.Services.Interfaces;

public interface IRepositoryService<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
    IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> predicate);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}
