using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IFinanceInfoRepository
{
    public void Put(FinanceInfo financeInfo);
    public Task<FinanceInfo?> Get(CancellationToken token = default);
}
