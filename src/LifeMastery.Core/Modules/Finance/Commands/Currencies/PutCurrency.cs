using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Currencies;

public class PutCurrencyRequest
{
    public int? Id { get; init; }
    public required string Name { get; init; }
}

public class PutCurrency(
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) : CommandBase<PutCurrencyRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutCurrencyRequest command, CancellationToken token = default)
    {
        if (command.Id is null)
        {
            currencyRepository.Put(new Currency(command.Name));
        }
        else
        {
            var currency = await currencyRepository.Get(command.Id.Value, token)
                ?? throw new ApplicationException($"Currency with ID '{command.Id}' was not found.");

            currency.Name = command.Name;
        }
    }
}