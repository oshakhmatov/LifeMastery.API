using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.Repositories;

public interface IWeightRecordRepository
{
    public Task<WeightRecord[]> List();
    public Task<WeightRecord?> GetLast();
    public Task<WeightRecord?> Get(DateOnly date);
    public void Put(WeightRecord weightRecord);
    public void Remove(WeightRecord weightRecord);
}
