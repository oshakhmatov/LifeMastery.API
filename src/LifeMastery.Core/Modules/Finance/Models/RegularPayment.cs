using LifeMastery.Core.Modules.Finance.Enums;

namespace LifeMastery.Core.Modules.Finance.Models;

public class RegularPayment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public Period Period { get; set; }
    public int? DeadlineDay { get; private set; }
    public int? DeadlineMonth { get; private set; }
    public bool IsAdvanced { get; set; }

    private readonly IList<Payment> payments;
    public IReadOnlyCollection<Payment> Payments => payments.AsReadOnly();

    protected RegularPayment() { }

    public RegularPayment(string name, bool isAdvanced, Period period, int? deadlineDay, int? deadlineMonth, decimal? amount)
    {
        Name = name;
        Amount = amount;
        Period = period;
        IsAdvanced = isAdvanced;

        SetDeadline(deadlineDay, deadlineMonth);
    }

    public void SetDeadline(int? deadlineDay, int? deadlineMonth)
    {
        if (deadlineDay == null)
            throw new Exception("Deadline day is required.");

        if (Period == Period.Year && deadlineMonth == null)
            throw new Exception("Deadline month is required.");

        DeadlineDay = deadlineDay;
        DeadlineMonth = deadlineMonth;
    }

    public Payment GetPayment(int paymentId)
    {
        return Payments.FirstOrDefault(r => r.Id == paymentId)
            ?? throw new Exception($"Payment with ID='{paymentId}' was not found.");
    }

    public void AddPayment(Payment payment)
    {
        payments.Add(payment);
    }

    public void RemovePaymentById(int id)
    {
        var rule = Payments.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception($"Payment with ID='{id}' was not found.");

        payments.Remove(rule);
    }

    public bool GetIsPaid()
    {
        if (payments.Count == 0)
            return false;

        var today = DateTime.UtcNow;
        var currentMonth = today.Month;
        var currentYear = today.Year;

        if (IsAdvanced)
        {
            if (Period == Period.Month)
            {
                var payment = payments
                    .Where(p => p.PeriodYear == currentYear && p.PeriodMonth == currentMonth)
                    .FirstOrDefault();

                return payment is not null;
            }

            if (Period == Period.Year)
            {
                var payment = payments
                    .Where(p => p.PeriodYear == currentYear)
                    .FirstOrDefault();

                return payment is not null;
            }
        }
        else
        {
            if (Period == Period.Month)
            {
                var payment = payments
                    .Where(p => p.PeriodYear == currentYear && p.PeriodMonth == currentMonth - 1)
                    .FirstOrDefault();

                return payment is not null;
            }

            if (Period == Period.Year)
            {
                var payment = payments
                    .Where(p => p.PeriodYear == currentYear - 1)
                    .FirstOrDefault();

                return payment is not null;
            }
        }

        throw new NotImplementedException();
    }
}
