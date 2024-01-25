using LifeMastery.Core.Modules.Jobs.Models;

namespace LifeMastery.Core.Modules.Jobs.Repositories;

public interface IJobRepository
{
    public void Add(Job job);
    public void Remove(Job job);
    public void Update(Job job);

    public Task<Job?> GetById(int id);
    public Task<Job[]> Get(bool completed);
}
