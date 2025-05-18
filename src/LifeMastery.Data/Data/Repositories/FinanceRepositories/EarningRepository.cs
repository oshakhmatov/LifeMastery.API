using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class EarningRepository(AppDbContext db) : Repository<Earning>(db), IEarningRepository
{
    public async Task<Earning?> GetLatestByFamilyMember(FamilyMember familyMember, CancellationToken cancellationToken)
    {
        return await db.Earnings
            .Include(e => e.FamilyMember)
            .Where(e => e.FamilyMember.Id == familyMember.Id)
            .OrderByDescending(e => e.PeriodYear)
            .ThenByDescending(e => e.PeriodMonth)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Earning[]> GetByPeriodAsync(int year, int month, CancellationToken token = default)
    {
        return await db.Earnings
            .Where(e => e.PeriodYear == year && e.PeriodMonth == month)
            .Include(e => e.FamilyMember)
            .OrderBy(e => e.Id)
            .ToArrayAsync(token);
    }
}
