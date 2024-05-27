using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public class FinanceInfoRepository : RepositoryBase<FinanceInfo>, IFinanceInfoRepository
{
    public FinanceInfoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<FinanceInfo?> Get(CancellationToken token = default)
    {
        return await dbContext.FinanceInfo
            .FirstOrDefaultAsync(token);
    }
}
