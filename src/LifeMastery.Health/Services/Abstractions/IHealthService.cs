using LifeMastery.Health.DataTransferObjects;

namespace LifeMastery.Health.Services.Abstractions;

public interface IHealthService
{
    WeightInfoDto GetWeightInfo(double weight, int height);
}