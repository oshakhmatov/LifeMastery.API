using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

namespace LifeMastery.Core.Modules.WeightControl.Services.Abstractions;

public interface IHealthService
{
    WeightInfoDto GetWeightInfo(double weight, int height);
}