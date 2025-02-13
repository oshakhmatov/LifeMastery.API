using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

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

    public static ExpenseDto FromModel(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            CategoryId = expense.Category?.Id,
            CurrencyId = expense.Currency?.Id,
            CategoryName = expense.Category?.Name,
            Note = expense.Note,
            Date = expense.Date,
            //Source = expense.Source,
            Source = expense.ParsedPlace,
            ParsedPlace = expense.ParsedPlace
        };
    }
}
