using LifeMastery.Core.Modules.Finance.Enums;

namespace LifeMastery.Core.Modules.Finance.Models;

public class FamilyBudgetRule
{
    public int Id { get; set; }
    public ContributionRatio ContributionRatio { get; set; }
    public int PeriodYear { get; set; }
    public int PeriodMonth { get; set; }

    public FamilyBudgetRule(ContributionRatio contributionRatio, int periodYear, int periodMonth)
    {
        ContributionRatio = contributionRatio;
        PeriodYear = periodYear;
        PeriodMonth = periodMonth;
    }

    protected FamilyBudgetRule() { }
}
