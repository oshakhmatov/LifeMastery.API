using LifeMastery.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LifeMastery.Infrastructure.Data;

public class Repository<T>(AppDbContext db) : IRepository<T> where T : class
{
    protected readonly AppDbContext db = db;

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return db.Set<T>().FindAsync([id], cancellationToken).AsTask();
    }

    public Task<T[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return db.Set<T>().ToArrayAsync(cancellationToken);
    }

    public Task<T[]> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
    {
        return db.Set<T>().Where(predicate).ToArrayAsync(token);
    }

    public void Add(T entity)
    {
        db.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        db.Set<T>().Remove(entity);
    }

    public IQueryable<T> Query()
    {
        return db.Set<T>();
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return db.Set<T>().AnyAsync(predicate, cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return db.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }
}
