using LifeMastery.Domain.Abstractions;
using LifeMastery.Health.DataTransferObjects;
using LifeMastery.Health.Models;
using LifeMastery.Health.Repositories;
using LifeMastery.Health.Services.Abstractions;
using System.Globalization;

namespace LifeMastery.Health.Commands;

public sealed class GetWeightControlData(
    IObjectMapper mapper,
    IRepository<HealthInfo> healthInfo,
    IWeightRecordRepository weightRecordRepository,
    IHealthService healthService,
    IStatisticService statisticService) : ICommand<Unit, WeightControlViewModel>
{
    public async Task<WeightControlViewModel> Execute(Unit _, CancellationToken token)
    {
        var healthInfoItem = await healthInfo.FirstOrDefaultAsync(_ => true, token);
        var weightRecords = await weightRecordRepository.GetAllOrderedByDateAsync();
        var last = weightRecords.LastOrDefault();

        var result = new WeightControlViewModel
        {
            HealthInfo = mapper.Map<HealthInfoDto>(healthInfoItem),
            LastWeightRecord = mapper.Map<WeightRecordDto>(last)
        };

        if (weightRecords.Length > 0)
        {
            result.WeightChart = new WeightChartDto
            {
                Labels = weightRecords.Select(r => r.Date.ToString("d MMMM", new CultureInfo("ru-RU"))).ToArray(),
                Values = weightRecords.Select(r => r.Weight).ToArray()
            };

            result.WeightRecords = mapper.Map<WeightRecordDto[]>(weightRecords.Reverse().Take(30));
            result.StatisticalData = statisticService.GetStatisticalData(weightRecords);
        }

        if (last is not null && healthInfoItem is not null)
        {
            result.WeightInfo = healthService.GetWeightInfo(last.Weight, healthInfoItem.Height);
        }

        return result;
    }
}
