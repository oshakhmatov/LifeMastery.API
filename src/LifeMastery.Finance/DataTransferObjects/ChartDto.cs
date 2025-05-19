namespace LifeMastery.Finance.DataTransferObjects;

public record ChartDto(
    string[] Labels,
    long[] Values,
    string?[] Colors);
