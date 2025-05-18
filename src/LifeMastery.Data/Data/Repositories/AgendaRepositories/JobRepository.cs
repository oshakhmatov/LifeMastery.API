using LifeMastery.Agenda.Models;
using LifeMastery.Agenda.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.AgendaRepositories;

public sealed class JobRepository(AppDbContext db) : Repository<Job>(db), IJobRepository
{
    public async Task<Job[]> GetAllNotCompletedAsync(CancellationToken token)
    {
        return await db.Jobs
            .Where(x => x.IsCompleted == false)
            .OrderBy(x => x.Priority)
            .ThenBy(x => x.Deadline)
            .ThenBy(x => x.Name)
            .ToArrayAsync(token);
    }
}
