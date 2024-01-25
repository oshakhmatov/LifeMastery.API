using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.WeightControlRepositories;

public class WeightRecordRepository : RepositoryBase<WeightRecord>, IWeightRecordRepository
{
    public WeightRecordRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<WeightRecord?> Get(DateOnly date)
    {
        return await dbContext.WeightRecords
            .Where(x => x.Date == date)
            .FirstOrDefaultAsync();
    }

    public async Task<WeightRecord?> GetLast()
    {
        return await dbContext.WeightRecords
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<WeightRecord[]> List()
    {
        return await dbContext.WeightRecords
            .OrderBy(x => x.Date)
            .ToArrayAsync();
    }
}
