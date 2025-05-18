using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Currencies;

public sealed class RemoveCurrency(IRepository<Currency> currencies, IUnitOfWork unitOfWork) : ICommand<RemoveCurrency.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var currency = await currencies.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Currency with ID '{request.Id}' was not found.");

        currencies.Remove(currency);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
