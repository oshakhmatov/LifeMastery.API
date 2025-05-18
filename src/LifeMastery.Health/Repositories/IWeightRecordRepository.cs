using LifeMastery.Domain.Abstractions;
using LifeMastery.Health.Models;

namespace LifeMastery.Health.Repositories;

public interface IWeightRecordRepository : IRepository<WeightRecord>
{
    public Task<WeightRecord[]> GetAllOrderedByDateAsync();
}
