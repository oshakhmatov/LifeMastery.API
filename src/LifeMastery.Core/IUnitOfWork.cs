namespace LifeMastery.Core;

public interface IUnitOfWork
{
    public Task Commit();
}
