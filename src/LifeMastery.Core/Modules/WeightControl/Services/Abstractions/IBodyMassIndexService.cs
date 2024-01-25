namespace LifeMastery.Core.Modules.WeightControl.Services.Abstractions;

public interface IBodyMassIndexService
{
    public double CalculateBmi(double weight, int height);
    public string GetDiagnose(double bodyMassIndex);
    public string GetHealthAdvise(double minWeight, double maxWeight, double perfectWeight, double actualWeight);
    public double GetHealthyMaxWeight(int height);
    public double GetHealthyMinWeight(int height);
    public double GetPerfectWeight(int height);
}