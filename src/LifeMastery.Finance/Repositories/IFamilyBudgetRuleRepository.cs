using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Repositories;

public interface IFamilyBudgetRuleRepository : IRepository<FamilyBudgetRule>
{
    Task<FamilyBudgetRule[]> GetByPeriodAsync(int year, int month, CancellationToken token = default);
    Task<FamilyBudgetRule?> GetLatest(CancellationToken cancellationToken);
}
