namespace LifeMastery.Finance.DataTransferObjects;

public record ExpenseDto(
    int Id,
    decimal Amount,
    DateOnly Date,
    int? CategoryId,
    int? CurrencyId,
    string? CategoryName,
    string? Note,
    string? Source,
    string? ParsedPlace);
