using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.WeightControlRepositories;

public sealed class HealthInfoRepository : RepositoryBase<HealthInfo>, IHealthInfoRepository
{
    public HealthInfoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<HealthInfo?> Get()
    {
        return await dbContext.HealthInfos
            .Where(x => x.UserId == 1)
            .FirstOrDefaultAsync();
    }
}
