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
    public List<Payment> Payments { get; set; }

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
}
