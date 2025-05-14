using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class FamilyBudgetRuleDto
{
    public int Id { get; set; }
    public string ContributionRatio { get; set; }
}


public static class FamilyBudgetRuleProjections
{
    public static FamilyBudgetRuleDto ToDto(this FamilyBudgetRule familyBudgetRule)
    {
        return new FamilyBudgetRuleDto
        {
            Id = familyBudgetRule.Id,
            ContributionRatio = GetContributionRatioName(familyBudgetRule.ContributionRatio)
        };
    }

    private static string GetContributionRatioName(ContributionRatio contributionRatio)
    {
        return contributionRatio switch
        {
            ContributionRatio.Equal => "Поровну",
            ContributionRatio.Proportional => "Пропорционально",
            _ => throw new ArgumentOutOfRangeException(nameof(contributionRatio), contributionRatio, null)
        };
    }
}
