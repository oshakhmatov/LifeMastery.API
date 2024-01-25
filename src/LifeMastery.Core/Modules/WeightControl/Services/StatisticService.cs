using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;
using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Services.Abstractions;

namespace LifeMastery.Core.Modules.WeightControl.Services;

public sealed class StatisticService : IStatisticService
{
    public StatisticalDataDto GetStatisticalData(WeightRecord[] weightRecords)
    {
        var lastWeightRecord = weightRecords.LastOrDefault();
        var result = new StatisticalDataDto();

        if (weightRecords.Length >= 30)
        {
            result.MonthWeightLost = Math.Round(weightRecords.SkipLast(29).Last().Weight - lastWeightRecord!.Weight, 1);
        }

        if (weightRecords.Length >= 7)
        {
            result.WeekWeightLost = Math.Round(weightRecords.SkipLast(6).Last().Weight - lastWeightRecord!.Weight, 1);
        }

        if (weightRecords.Length >= 2)
        {
            result.TotalWeightLost = Math.Round(weightRecords.First().Weight - lastWeightRecord!.Weight, 1);
        }

        return result;
    }
}
