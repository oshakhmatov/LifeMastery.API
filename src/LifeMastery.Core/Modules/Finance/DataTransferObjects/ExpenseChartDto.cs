namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseChartDto
{
    public required string[] Labels { get; init; }
    public required long[] Values { get; init; }
    public required string?[] Colors { get; init; }
}