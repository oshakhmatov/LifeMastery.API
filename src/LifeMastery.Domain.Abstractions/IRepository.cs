using System.Linq.Expressions;

namespace LifeMastery.Domain.Abstractions;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T[]> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T[]> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
    void Add(T entity);
    void Remove(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<T> Query();
}
