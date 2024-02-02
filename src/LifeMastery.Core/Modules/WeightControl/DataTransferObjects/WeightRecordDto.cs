using LifeMastery.Core.Modules.WeightControl.Models;
using System.Globalization;

namespace LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

public class WeightRecordDto
{
    public double Weight { get; set; }
    public string Date { get; set; }

    public static WeightRecordDto FromModel(WeightRecord weightRecord)
    {
        return new WeightRecordDto
        {
            Date = weightRecord.Date.ToString("dd.MM.yyyy", new CultureInfo("ru-RU")),
            Weight = weightRecord.Weight
        };
    }
}
