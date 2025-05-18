using LifeMastery.Agenda.Models;
using LifeMastery.Domain.Abstractions;

namespace LifeMastery.Agenda.Repositories;

public interface IJobRepository : IRepository<Job>
{
    Task<Job[]> GetAllNotCompletedAsync(CancellationToken token);
}
