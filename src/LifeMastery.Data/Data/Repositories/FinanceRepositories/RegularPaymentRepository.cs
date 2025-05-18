using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class RegularPaymentRepository(AppDbContext db) : Repository<RegularPayment>(db), IRegularPaymentRepository
{
    public async Task<RegularPayment[]> GetAllOrderedByNewestAsync(CancellationToken token)
    {
        return await db.RegularPayments
            .Include(e => e.Payments)
            .OrderByDescending(e => e.Id)
            .ToArrayAsync(token);
    }
}
