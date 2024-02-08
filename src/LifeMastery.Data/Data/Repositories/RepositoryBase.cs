namespace LifeMastery.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<TEntity> where TEntity : class
{
    private protected readonly AppDbContext dbContext;

    public RepositoryBase(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Remove(TEntity entity)
    {
        dbContext.Remove(entity);
    }

    public void Put(TEntity entity)
    {
        dbContext.Add(entity);
    }
}