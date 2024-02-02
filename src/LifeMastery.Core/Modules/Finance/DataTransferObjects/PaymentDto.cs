using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class PaymentDto
{
    public int Id { get; set; }
    public int RegularPaymentId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }
    public string PeriodName { get; set; }
    
    public static PaymentDto FromModel(Payment payment, int regularPaymentId)
    {
        return new PaymentDto
        {
            Id = payment.Id,
            RegularPaymentId = regularPaymentId,
            Date = payment.Date,
            Amount = payment.Amount,
            PeriodYear = payment.PeriodYear,
            PeriodMonth = payment.PeriodMonth,
            PeriodName = GetPeriodName(payment.PeriodYear, payment.PeriodMonth)
        };
    }

    private static string GetPeriodName(int periodYear, int? periodMonth)
    {
        if (periodMonth == null)
        {
            return periodYear.ToString();
        }

        return $"{DateHelper.GetMonthName(periodMonth.Value)} {periodYear}";
    }
}