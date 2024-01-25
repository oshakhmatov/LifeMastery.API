using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.WeightControl.Services.Abstractions;

namespace LifeMastery.Core.Modules.WeightControl.Services;

public sealed class BodyMassIndexService : IBodyMassIndexService
{
    public double CalculateBmi(double weight, int height)
    {
        return MathHelper.Round(weight / (height / 100d * height / 100d));
    }

    public double GetPerfectWeight(int height)
    {
        return MathHelper.Round(21.7 * (height / 100d * height / 100d));
    }

    public double GetHealthyMinWeight(int height)
    {
        return MathHelper.Round(18.4 * (height / 100d * height / 100d));
    }

    public double GetHealthyMaxWeight(int height)
    {
        return MathHelper.Round(24.9 * (height / 100d * height / 100d));
    }

    public string GetDiagnose(double bodyMassIndex)
    {
        return bodyMassIndex switch
        {
            < 18.5 => "Недовес",
            < 25 => "Нормальный вес",
            < 30 => "Предожирение",
            < 35 => "Ожирение 1 ст.",
            < 40 => "Ожирение 2 ст.",
            _ => "Ожирение 3 ст."
        };
    }

    public string GetHealthAdvise(double minWeight, double maxWeight, double perfectWeight, double actualWeight)
    {
        string adviseStrengthWord;
        string adviseGoalWord;
        double adviseGoalWeight;

        if (actualWeight > maxWeight)
        {
            adviseStrengthWord = "Стоит";
            adviseGoalWord = "сбросить";
            adviseGoalWeight = actualWeight - maxWeight;
        }
        else if (actualWeight < minWeight)
        {
            adviseStrengthWord = "Стоит";
            adviseGoalWord = "набрать";
            adviseGoalWeight = minWeight - actualWeight;
        }
        else if (actualWeight > perfectWeight)
        {
            adviseStrengthWord = "Можно";
            adviseGoalWord = "сбросить";
            adviseGoalWeight = actualWeight - perfectWeight;
        }
        else
        {
            adviseStrengthWord = "Можно";
            adviseGoalWord = "набрать";
            adviseGoalWeight = perfectWeight - actualWeight;
        }

        return $"{adviseStrengthWord} {adviseGoalWord} {MathHelper.Round(adviseGoalWeight)} кг";
    }
}
