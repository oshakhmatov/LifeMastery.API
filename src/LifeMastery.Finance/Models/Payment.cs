using LifeMastery.Common;

namespace LifeMastery.Finance.Models;

public class Payment
{
    public int Id { get; private set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }
    public int RegularPaymentId { get; set; }

    public Payment(decimal amount, DateOnly date, int periodYear, int? periodMonth)
    {
        Amount = amount;
        Date = date;
        PeriodYear = periodYear;
        PeriodMonth = periodMonth;
    }

    public string GetPeriodName()
    {
        if (PeriodMonth == null)
        {
            return PeriodYear.ToString();
        }

        return $"{DateHelper.GetMonthName(PeriodMonth.Value)} {PeriodYear}";
    }

    protected Payment() { }
}
