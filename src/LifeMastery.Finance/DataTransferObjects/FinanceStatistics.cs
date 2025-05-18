namespace LifeMastery.Finance.DataTransferObjects;

public class FinanceStatisticsDto
{
    public required decimal? RemainingAmountPercent { get; set; }
    public required decimal? FoodSpendingPercent { get; init; }
    public required decimal? OverallTaxPercent { get; init; }
}