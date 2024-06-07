using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Info;

public class PutFinanceInfoCommand
{
    public required decimal? Income { get; init; }
}

public class PutFinanceInfo(
    IUnitOfWork unitOfWork,
    IFinanceInfoRepository financeInfoRepository) : CommandBase<PutFinanceInfoCommand>(unitOfWork)
{
    protected override async Task OnExecute(PutFinanceInfoCommand command, CancellationToken token = default)
    {
        var info = await financeInfoRepository.Get(token);
        if (info == null)
        {
            financeInfoRepository.Put(new FinanceInfo
            {
                Income = command.Income
            });
        }
        else
        {
            info.Income = command.Income;
        }
    }
}
