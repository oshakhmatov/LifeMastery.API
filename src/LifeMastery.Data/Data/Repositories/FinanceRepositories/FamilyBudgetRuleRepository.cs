using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class FamilyBudgetRuleRepository(AppDbContext dbContext) : RepositoryBase<FamilyBudgetRule>(dbContext), IFamilyBudgetRuleRepository
{
    public async Task<FamilyBudgetRule?> Get(int id, CancellationToken token = default)
    {
        return await dbContext.FamilyBudgetRules
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<FamilyBudgetRule?> GetLatest(CancellationToken cancellationToken)
    {
        return await dbContext.FamilyBudgetRules
            .OrderByDescending(e => e.PeriodYear)
            .ThenByDescending(e => e.PeriodMonth)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FamilyBudgetRule[]> List(int year, int month, CancellationToken token = default)
    {
        return await dbContext.FamilyBudgetRules
            .Where(e => e.PeriodYear == year && e.PeriodMonth == month)
            .OrderBy(e => e.Id)
            .ToArrayAsync(token);
    }
}
