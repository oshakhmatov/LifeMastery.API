using LifeMastery.Common;
using LifeMastery.Finance.Enums;

namespace LifeMastery.Finance.Models;

public class RegularPayment
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public Period Period { get; set; }
    public int? DeadlineDay { get; private set; }
    public int? DeadlineMonth { get; private set; }
    public int? PayFromDay { get; set; }
    public bool IsAdvanced { get; set; }
    public bool IsTax { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = [];

    public RegularPayment(string name, bool isAdvanced, bool isTax, Period period, int? deadlineDay, int? deadlineMonth, decimal? amount, int? payFromDay)
    {
        Name = name;
        Amount = amount;
        Period = period;
        IsAdvanced = isAdvanced;
        PayFromDay = payFromDay;
        IsTax = isTax;

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
        Payments.Add(payment);
    }

    public void RemovePaymentById(int id)
    {
        var rule = Payments.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception($"Payment with ID='{id}' was not found.");

        Payments.Remove(rule);
    }

    public bool IsPaid()
    {
        var today = DateTime.UtcNow;
        var currentDay = today.Day;
        var currentMonth = today.Month;
        var currentYear = today.Year;

        if (PayFromDay is not null && currentDay < PayFromDay)
            return true;

        if (Payments.Count == 0)
            return false;

        if (IsAdvanced)
        {
            if (Period == Period.Month)
            {
                var payment = Payments
                    .Where(p => p.PeriodYear == currentYear && p.PeriodMonth == currentMonth)
                    .FirstOrDefault();

                return payment is not null;
            }

            if (Period == Period.Year)
            {
                var payment = Payments
                    .Where(p => p.PeriodYear == currentYear)
                    .FirstOrDefault();

                return payment is not null;
            }
        }
        else
        {
            if (Period == Period.Month)
            {
                var payment = Payments
                    .Where(p => p.PeriodYear == currentYear && p.PeriodMonth == currentMonth - 1)
                    .FirstOrDefault();

                return payment is not null;
            }

            if (Period == Period.Year)
            {
                var payment = Payments
                    .Where(p => p.PeriodYear == currentYear - 1)
                    .FirstOrDefault();

                return payment is not null;
            }
        }

        throw new NotImplementedException();
    }

    public decimal? GetApproximateAmount()
    {
        if (Amount != null || Payments.Count == 0)
        {
            return 0;
        }

        return MathHelper.Round(Payments.Average(p => p.Amount));
    }

    protected RegularPayment() { }
}
