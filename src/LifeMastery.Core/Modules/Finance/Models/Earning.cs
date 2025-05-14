namespace LifeMastery.Core.Modules.Finance.Models;

public class Earning
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int PeriodYear { get; set; }
    public int PeriodMonth { get; set; }
    public FamilyMember FamilyMember { get; set; }

    public Earning(decimal amount, int periodYear, int periodMonth, FamilyMember familyMember)
    {
        Amount = amount;
        PeriodYear = periodYear;
        PeriodMonth = periodMonth;
        FamilyMember = familyMember;
    }

    protected Earning() { }
}
