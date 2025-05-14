using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IFamilyBudgetRuleRepository
{
    Task<FamilyBudgetRule[]> List(int year, int month, CancellationToken token);
    Task<FamilyBudgetRule?> Get(int id, CancellationToken token);
    public void Put(FamilyBudgetRule earning);
    Task<FamilyBudgetRule?> GetLatest(CancellationToken cancellationToken);
}
