namespace LifeMastery.Finance.DataTransferObjects;

public record FinanceStatisticsDto(
    decimal? RemainingAmountPercent,
    decimal? FoodSpendingPercent,
    decimal? OverallTaxPercent);
