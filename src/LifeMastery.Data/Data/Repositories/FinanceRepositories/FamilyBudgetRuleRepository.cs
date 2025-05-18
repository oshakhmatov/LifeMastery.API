using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class FamilyBudgetRuleRepository(AppDbContext db) : Repository<FamilyBudgetRule>(db), IFamilyBudgetRuleRepository
{

    public async Task<FamilyBudgetRule?> GetLatest(CancellationToken cancellationToken)
    {
        return await db.FamilyBudgetRules
            .OrderByDescending(e => e.PeriodYear)
            .ThenByDescending(e => e.PeriodMonth)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FamilyBudgetRule[]> GetByPeriodAsync(int year, int month, CancellationToken token = default)
    {
        return await db.FamilyBudgetRules
            .Where(e => e.PeriodYear == year && e.PeriodMonth == month)
            .OrderBy(e => e.Id)
            .ToArrayAsync(token);
    }
}
