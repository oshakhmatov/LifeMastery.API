using LifeMastery.Core.Modules.Jobs.Models;

namespace LifeMastery.Core.Modules.Jobs.Repositories;

public interface IJobRepository
{
    public void Remove(Job job);
    public void Put(Job job);

    public Task<Job?> GetById(int id);
    public Task<Job[]> Get(bool completed);
}
