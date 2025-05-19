namespace LifeMastery.Finance.DataTransferObjects;

public record ParsedExpenseDto(
    decimal Amount,
    string Place,
    string Currency,
    DateOnly Date,
    string TransactionId,
    string Source);
