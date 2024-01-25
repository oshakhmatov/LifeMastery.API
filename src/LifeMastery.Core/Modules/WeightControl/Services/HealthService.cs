using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;
using LifeMastery.Core.Modules.WeightControl.Services.Abstractions;

namespace LifeMastery.Core.Modules.WeightControl.Services;

public class HealthService : IHealthService
{
    private readonly IBodyMassIndexService bodyMassIndexService;

    public HealthService(IBodyMassIndexService bodyMassIndexService)
    {
        this.bodyMassIndexService = bodyMassIndexService;
    }

    public WeightInfoDto GetWeightInfo(double weight, int height)
    {
        var bodyMassIndex = bodyMassIndexService.CalculateBmi(weight, height);
        var maxWeight = bodyMassIndexService.GetHealthyMaxWeight(height);
        var minWeight = bodyMassIndexService.GetHealthyMinWeight(height);
        var perfectWeight = bodyMassIndexService.GetPerfectWeight(height);
        var advise = bodyMassIndexService.GetHealthAdvise(minWeight, maxWeight, perfectWeight, weight);
        var diagnose = bodyMassIndexService.GetDiagnose(bodyMassIndex);

        return new WeightInfoDto
        {
            Advise = advise,
            Diagnose = diagnose,
            BodyMassIndex = bodyMassIndex,
            MaxWeight = maxWeight,
            PerfectWeight = perfectWeight,
            MinWeight = minWeight
        };
    }
}
