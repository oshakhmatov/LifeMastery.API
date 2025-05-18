namespace LifeMastery.Finance.DataTransferObjects;

public class ChartDto
{
    public required string[] Labels { get; init; }
    public required long[] Values { get; init; }
    public required string?[] Colors { get; init; }
}