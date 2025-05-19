using LifeMastery.Finance.Enums;

namespace LifeMastery.Finance.Models;

public class FamilyBudgetRule
{
    public int Id { get; private set; }
    public ContributionRatio ContributionRatio { get; set; }
    public int PeriodYear { get; set; }
    public int PeriodMonth { get; set; }

    public FamilyBudgetRule(ContributionRatio contributionRatio, int periodYear, int periodMonth)
    {
        ContributionRatio = contributionRatio;
        PeriodYear = periodYear;
        PeriodMonth = periodMonth;
    }

    public string GetContributionRatioName()
    {
        return ContributionRatio switch
        {
            ContributionRatio.Equal => "Поровну",
            ContributionRatio.Proportional => "Пропорционально",
            _ => throw new ArgumentOutOfRangeException(nameof(ContributionRatio), ContributionRatio, null)
        };
    }

    protected FamilyBudgetRule() { }
}
