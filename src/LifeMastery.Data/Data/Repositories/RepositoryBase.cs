﻿namespace LifeMastery.Infrastructure.Data.Repositories;

public abstract class RepositoryBase<TEntity>
{
    private protected readonly AppDbContext dbContext;

    public RepositoryBase(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        dbContext.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Update(entity);
    }
}
