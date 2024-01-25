namespace LifeMastery.Core.Modules.Finance.Models;

public class Expense
{
    public int Id { get; }
    public decimal Amount { get; set; }
    public ExpenseCategory? Category { get; set; }
    public string? Note { get; set; }
    public DateOnly Date { get; set; }
    public string? Source { get; set; }

    protected Expense() { }

    public Expense(decimal amount)
    {
        Amount = amount;
        Date = DateOnly.FromDateTime(DateTime.Today);
    }
}
