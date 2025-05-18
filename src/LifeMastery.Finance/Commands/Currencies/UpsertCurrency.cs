using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Currencies;

public sealed class UpsertCurrency(IRepository<Currency> currencies, IUnitOfWork unitOfWork) : ICommand<UpsertCurrency.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        if (request.Id is null)
        {
            currencies.Add(new Currency(request.Name));
        }
        else
        {
            var currency = await currencies.GetByIdAsync(request.Id.Value, token)
                ?? throw new AppException($"Currency with ID '{request.Id}' was not found.");

            currency.Name = request.Name;
        }

        await unitOfWork.Commit(token);
    }

    public record Request(int? Id, string Name);
}
