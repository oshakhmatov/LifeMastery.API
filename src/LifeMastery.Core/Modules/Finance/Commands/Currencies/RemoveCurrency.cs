using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Currencies;

public sealed class RemoveCurrency(
    IUnitOfWork unitOfWork,
    ICurrencyRepository currencyRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expense = await currencyRepository.Get(id, token)
            ?? throw new Exception($"Currency with ID={id} was not found.");

        currencyRepository.Remove(expense);
    }
}