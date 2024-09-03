namespace LifeMastery.Core.Modules.Finance.Models;

public class Expense
{
    public int Id { get; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public DateOnly Date { get; set; }
    public string? Source { get; set; }
    public string? ParsedPlace { get; set; }

    public Currency? Currency { get; set; }
    public ExpenseCategory? Category { get; set; }
    public EmailSubscription? EmailSubscription { get; set; }

    protected Expense() { }

    public Expense(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
        Date = DateOnly.FromDateTime(DateTime.Today);
    }
}
