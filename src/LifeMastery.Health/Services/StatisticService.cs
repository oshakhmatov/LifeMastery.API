﻿using LifeMastery.Common;
using LifeMastery.Health.DataTransferObjects;
using LifeMastery.Health.Models;
using LifeMastery.Health.Services.Abstractions;

namespace LifeMastery.Health.Services;

public sealed class StatisticService : IStatisticService
{
    public StatisticalDataDto GetStatisticalData(WeightRecord[] weightRecords)
    {
        var result = new StatisticalDataDto();

        if (weightRecords.Length == 0)
            return result;

        var lastWeightRecord = weightRecords.Last();

        var today = DateOnly.FromDateTime(DateTime.Now.Date);
        var monthAgoRecord = weightRecords.Where(wr => wr.Date < today.AddMonths(-1)).LastOrDefault();
        var weekAgoRecord = weightRecords.Where(wr => wr.Date < today.AddDays(-7)).LastOrDefault();

        if (monthAgoRecord is not null)
        {
            result.MonthWeightLost = MathHelper.Round(monthAgoRecord.Weight - lastWeightRecord!.Weight);
        }

        if (weekAgoRecord is not null)
        {
            result.WeekWeightLost = MathHelper.Round(weekAgoRecord.Weight - lastWeightRecord!.Weight);
        }

        if (weightRecords.Length >= 2)
        {
            result.TotalWeightLost = MathHelper.Round(weightRecords.First().Weight - lastWeightRecord!.Weight);
        }

        return result;
    }
}
