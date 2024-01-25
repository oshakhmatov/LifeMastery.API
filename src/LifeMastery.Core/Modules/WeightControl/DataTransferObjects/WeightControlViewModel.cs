namespace LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

public class WeightControlViewModel
{
    public StatisticalDataDto? StatisticalData { get; set; }
    public HealthInfoDto? HealthInfo { get; set; }
    public WeightRecordDto? LastWeightRecord { get; set; }
    public WeightInfoDto? WeightInfo { get; set; }
    public WeightChartDto? WeightChart { get; set; }
    public WeightRecordDto[]? WeightRecords { get; set; }
}
