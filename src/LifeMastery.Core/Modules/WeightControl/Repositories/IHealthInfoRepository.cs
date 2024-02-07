using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.Repositories;

public interface IHealthInfoRepository
{
    public Task<HealthInfo?> Get();
    void Put(HealthInfo healthInfo);
}
