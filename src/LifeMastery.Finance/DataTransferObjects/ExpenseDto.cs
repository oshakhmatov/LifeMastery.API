namespace LifeMastery.Finance.DataTransferObjects;

public class ExpenseDto
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required DateOnly Date { get; init; }
    public required int? CategoryId { get; init; }
    public required int? CurrencyId { get; init; }
    public required string? CategoryName { get; init; }
    public required string? Note { get; init; }
    public required string? Source { get; init; }
    public required string? ParsedPlace { get; init; }
}
