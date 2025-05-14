using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Earnings;

public class PutEarningRequest
{
    public int Id { get; init; }
    public required decimal Amount { get; init; }
}

public class PutEarning(
    IEarningRepository earningRepository,
    IUnitOfWork unitOfWork) : CommandBase<PutEarningRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutEarningRequest command, CancellationToken token = default)
    {
        var currency = await earningRepository.Get(command.Id, token)
                ?? throw new ApplicationException($"Earning with ID '{command.Id}' was not found.");

        currency.Amount = command.Amount;
    }
}