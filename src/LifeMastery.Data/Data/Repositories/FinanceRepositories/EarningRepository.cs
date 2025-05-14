using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class EarningRepository(AppDbContext dbContext) : RepositoryBase<Earning>(dbContext), IEarningRepository
{
    public async Task<Earning?> Get(int id, CancellationToken token = default)
    {
        return await dbContext.Earnings
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<Earning?> GetLatestByFamilyMember(FamilyMember familyMember, CancellationToken cancellationToken)
    {
        return await dbContext.Earnings
            .Include(e => e.FamilyMember)
            .Where(e => e.FamilyMember.Id == familyMember.Id)
            .OrderByDescending(e => e.PeriodYear)
            .ThenByDescending(e => e.PeriodMonth)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Earning[]> List(int year, int month, CancellationToken token = default)
    {
        return await dbContext.Earnings
            .Where(e => e.PeriodYear == year && e.PeriodMonth == month)
            .Include(e => e.FamilyMember)
            .OrderBy(e => e.Id)
            .ToArrayAsync(token);
    }
}
