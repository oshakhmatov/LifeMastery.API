namespace LifeMastery.Domain.Abstractions;

public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken = default);
}
