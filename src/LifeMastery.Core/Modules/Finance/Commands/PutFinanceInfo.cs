using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public class PutFinanceInfoCommand
{
    public required decimal? Income { get; init; }
}

public class PutFinanceInfo : CommandBase<PutFinanceInfoCommand>
{
    private readonly IFinanceInfoRepository financeInfoRepository;

    public PutFinanceInfo(IUnitOfWork unitOfWork, IFinanceInfoRepository financeInfoRepository) : base(unitOfWork)
    {
        this.financeInfoRepository = financeInfoRepository;
    }

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
