using LifeMastery.Health.DataTransferObjects;
using LifeMastery.Health.Models;

namespace LifeMastery.Health.Services.Abstractions
{
    public interface IStatisticService
    {
        StatisticalDataDto GetStatisticalData(WeightRecord[] weightRecords);
    }
}