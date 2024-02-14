namespace LifeMastery.Core.Common;

public abstract class CommandBase<T>
{
    private readonly IUnitOfWork unitOfWork;

    protected CommandBase(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(T request, CancellationToken token = default)
    {
        await OnExecute(request, token);
        await unitOfWork.Commit();
    }

    protected abstract Task OnExecute(T request, CancellationToken token);
}

public abstract class CommandBase
{
    private readonly IUnitOfWork unitOfWork;

    protected CommandBase(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(CancellationToken token = default)
    {
        await OnExecute(token);
        await unitOfWork.Commit();
    }

    protected abstract Task OnExecute(CancellationToken token);
}
