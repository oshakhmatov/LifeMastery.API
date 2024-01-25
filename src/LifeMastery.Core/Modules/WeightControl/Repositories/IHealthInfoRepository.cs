using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.Repositories;

public interface IHealthInfoRepository
{
    void Add(HealthInfo healthInfo);
    public Task<HealthInfo?> Get();
    void Update(HealthInfo healthInfo);
}
