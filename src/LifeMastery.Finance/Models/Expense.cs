namespace LifeMastery.Finance.Models;

public class Expense
{
    public int Id { get; private set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public DateOnly Date { get; set; }
    public string? Source { get; set; }
    public string? TransactionId { get; set; }
    public string? ParsedPlace { get; set; }

    public virtual Currency? Currency { get; set; }
    public virtual ExpenseCategory? Category { get; set; }
    public virtual EmailSubscription? EmailSubscription { get; set; }

    protected Expense() { }

    public Expense(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
        Date = DateOnly.FromDateTime(DateTime.Today);
    }

    public int? CurrencyId { get; private set; }
    public int? CategoryId { get; set; }
}
