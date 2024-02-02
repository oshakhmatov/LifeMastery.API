using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class RegularPaymentRepository : RepositoryBase<RegularPayment>, IRegularPaymentRepository
{
    public RegularPaymentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RegularPayment?> Get(int id, CancellationToken token)
    {
        return await dbContext.RegularPayments
            .Where(e => e.Id == id)
            .Include(e => e.Payments)
            .FirstOrDefaultAsync(token);
    }

    public async Task<RegularPayment[]> List(CancellationToken token)
    {
        return await dbContext.RegularPayments
            .Include(e => e.Payments)
            .OrderByDescending(e => e.Id)
            .ToArrayAsync(token);
    }
}
