using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;
using LifeMastery.Core.Modules.WeightControl.Repositories;
using LifeMastery.Core.Modules.WeightControl.Services.Abstractions;
using System.Globalization;

namespace LifeMastery.Core.Modules.WeightControl.Queries;

public sealed class GetWeightControlData
{
    private readonly IHealthInfoRepository healthInfoRepository;
    private readonly IWeightRecordRepository weightRecordRepository;
    private readonly IHealthService healthService;
    private readonly IStatisticService statisticService;

    public GetWeightControlData(
        IHealthInfoRepository healthInfoRepository,
        IWeightRecordRepository weightRecordRepository,
        IHealthService healthService,
        IStatisticService statisticService)
    {
        this.healthInfoRepository = healthInfoRepository;
        this.weightRecordRepository = weightRecordRepository;
        this.healthService = healthService;
        this.statisticService = statisticService;
    }

    public async Task<WeightControlViewModel> Execute()
    {
        var healthInfo = await healthInfoRepository.Get();
        var lastWeightRecords = await weightRecordRepository.List();
        var lastWeightRecord = lastWeightRecords.LastOrDefault();

        var result = new WeightControlViewModel
        {
            HealthInfo = healthInfo?.ToDto(),
            LastWeightRecord = lastWeightRecord?.ToDto()
        };

        if (lastWeightRecords.Length > 0)
        {
            result.WeightChart = new WeightChartDto
            {
                Labels = lastWeightRecords.Select(r => r.Date.ToString("d MMMM", new CultureInfo("ru-RU"))).ToArray(),
                Values = lastWeightRecords.Select(r => r.Weight).ToArray()
            };

            result.WeightRecords = lastWeightRecords
                .Reverse()
                .Take(30)
                .Select(wr => wr.ToDto())
                .ToArray();

            result.StatisticalData = statisticService.GetStatisticalData(lastWeightRecords);
        }

        if (lastWeightRecord is not null && healthInfo is not null)
        {
            result.WeightInfo = healthService.GetWeightInfo(lastWeightRecord.Weight, healthInfo.Height);
        }

        return result;
    }
}
