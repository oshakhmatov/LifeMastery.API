namespace LifeMastery.Domain.Abstractions;

public interface ICommand
{
    Task Execute(CancellationToken token);
}

public interface ICommand<TRequest>
{
    Task Execute(TRequest request, CancellationToken token);
}

public interface ICommand<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request, CancellationToken token);
}
