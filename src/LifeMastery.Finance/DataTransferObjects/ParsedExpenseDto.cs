namespace LifeMastery.Finance.DataTransferObjects;

public class ParsedExpenseDto
{
    public required decimal Amount { get; init; }
    public required string Place { get; init; }
    public required string Currency { get; init; }
    public required DateOnly Date { get; init; }
    public required string TransactionId { get; init; }
    public required string Source { get; init; }
}
