using LifeMastery.Common;

namespace LifeMastery.Health.Models;

public class WeightRecord
{
    public DateOnly Date { get; }
    public double Weight { get; private set; }

    public WeightRecord(double weight)
    {
        Date = DateOnly.FromDateTime(DateTime.Today);
        Weight = MathHelper.Round(weight);
    }

    public void SetWeight(double weight)
    {
        Weight = MathHelper.Round(weight);
    }

    protected WeightRecord() { }
}
