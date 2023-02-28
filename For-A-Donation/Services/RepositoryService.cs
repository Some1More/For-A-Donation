﻿using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class RepositoryService<TEntity> : IRepositoryService<TEntity> where TEntity : class, IEntity
{
    private readonly Context _context;
    private readonly DbSet<TEntity> _db;
    public RepositoryService(Context context)
    {
        _context = context;
        _db = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _db.ToList();
    }

    public TEntity GetById(int id)
    {
        return _db.AsNoTracking().SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> predicate)
    {
        return _db.Where(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(() => _db.Update(entity));
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.Run(() => _db.Remove(entity));
        await _context.SaveChangesAsync();
    }

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return Include(includeProperties);
    }

    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate);
    }

    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _db;
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}