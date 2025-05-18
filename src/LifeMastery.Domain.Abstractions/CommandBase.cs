namespace LifeMastery.Domain.Abstractions;

public abstract class CommandBase<TRequest, TResult>(IUnitOfWork? unitOfWork = null)
{
    public async Task<TResult> ExecuteAndCommit(TRequest request, CancellationToken token)
    {
        var result = await OnExecute(request, token);
        if (unitOfWork != null)
            await unitOfWork.Commit(token);
        return result;
    }

    protected abstract Task<TResult> OnExecute(TRequest request, CancellationToken token);
}


/// <summary>
/// Executes the command and commits all changes via Unit of Work.
/// </summary>
public abstract class CommandBase<T>(IUnitOfWork? unitOfWork = null)
{
    public async Task ExecuteAndCommit(T request, CancellationToken token)
    {
        await OnExecute(request, token);
        if (unitOfWork != null)
        {
            await unitOfWork.Commit(token);
        }
    }

    protected abstract Task OnExecute(T request, CancellationToken token);
}

/// <summary>
/// Executes the command and commits all changes via Unit of Work.
/// </summary>
public abstract class CommandBase(IUnitOfWork? unitOfWork = null)
{
    public async Task ExecuteAndCommit(CancellationToken token)
    {
        await OnExecute(token);
        if (unitOfWork != null)
        {
            await unitOfWork.Commit(token);
        }
    }

    protected abstract Task OnExecute(CancellationToken token);
}
