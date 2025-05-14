namespace LifeMastery.Core.Modules.Finance.Models;

public sealed class Payment
{
    public int Id { get; private set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }

    public Payment(decimal amount, DateOnly date, int periodYear, int? periodMonth)
    {
        Amount = amount;
        Date = date;
        PeriodYear = periodYear;
        PeriodMonth = periodMonth;
    }

    protected Payment() { }
}
