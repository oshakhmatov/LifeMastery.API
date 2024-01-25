using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

public class WeightRecordDto
{
    public double Weight { get; set; }
    public DateOnly Date { get; set; }

    public static WeightRecordDto FromModel(WeightRecord weightRecord)
    {
        return new WeightRecordDto
        {
            Date = weightRecord.Date,
            Weight = weightRecord.Weight
        };
    }
}
