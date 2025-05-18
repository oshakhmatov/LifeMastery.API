namespace LifeMastery.Health.DataTransferObjects;

public class WeightInfoDto
{
    public double? PerfectWeight { get; set; }
    public double? MinWeight { get; set; }
    public double? MaxWeight { get; set; }
    public double? BodyMassIndex { get; set; }
    public string? Advise { get; set; }
    public string? Diagnose { get; set; }
}
