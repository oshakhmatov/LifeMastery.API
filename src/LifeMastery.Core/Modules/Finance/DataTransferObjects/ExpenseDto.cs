using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Note { get; set; }

    public static ExpenseDto FromModel(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            CategoryId = expense.Category?.Id,
            CategoryName = expense.Category?.Name,
            Note = expense.Note,
            Date = expense.Date
        };
    }
}
