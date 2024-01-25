using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;
using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.Services.Abstractions
{
    public interface IStatisticService
    {
        StatisticalDataDto GetStatisticalData(WeightRecord[] weightRecords);
    }
}