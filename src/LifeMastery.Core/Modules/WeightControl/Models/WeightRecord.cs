using LifeMastery.Core.Common;

namespace LifeMastery.Core.Modules.WeightControl.Models;

public sealed class WeightRecord
{
    public DateOnly Date { get; }
    public double Weight { get; private set; }

    protected WeightRecord() { }

    public WeightRecord(double weight)
    {
        Date = DateOnly.FromDateTime(DateTime.Today);
        Weight = MathHelper.Round(weight);
    }

    public void SetWeight(double weight)
    {
        Weight = MathHelper.Round(weight);
    }
}
