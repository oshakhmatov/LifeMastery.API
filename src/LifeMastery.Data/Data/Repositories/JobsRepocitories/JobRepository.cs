using LifeMastery.Core.Modules.Jobs.Models;
using LifeMastery.Core.Modules.Jobs.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.JobsRepocitories;

public sealed class JobRepository : RepositoryBase<Job>, IJobRepository
{
    public JobRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Job?> GetById(int id)
    {
        return await dbContext.Jobs.FindAsync(id);
    }

    public async Task<Job[]> Get(bool completed)
    {
        return await dbContext.Jobs
            .Where(x => x.IsCompleted == completed)
            .OrderBy(x => x.Priority)
            .ThenBy(x => x.Deadline)
            .ThenBy(x => x.Name)
            .ToArrayAsync();
    }
}
