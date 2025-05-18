using LifeMastery.Health.Models;
using LifeMastery.Health.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.HealthRepositories;

public class WeightRecordRepository(AppDbContext dbContext) : Repository<WeightRecord>(dbContext), IWeightRecordRepository
{
    public async Task<WeightRecord[]> GetAllOrderedByDateAsync()
    {
        return await db.WeightRecords
            .OrderBy(x => x.Date)
            .ToArrayAsync();
    }
}
