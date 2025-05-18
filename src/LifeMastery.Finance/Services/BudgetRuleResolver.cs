using LifeMastery.Finance.Enums;
using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;

namespace LifeMastery.Finance.Services;

public class BudgetRuleResolver(IFamilyBudgetRuleRepository rules)
{
    public async Task<FamilyBudgetRule> Resolve(FamilyBudgetRule[] existing, int year, int month, CancellationToken token)
    {
        var rule = existing.FirstOrDefault(r => r.PeriodYear == year && r.PeriodMonth == month);
        if (rule != null)
            return rule;

        var latest = await rules.GetLatest(token);

        rule = new FamilyBudgetRule(
            latest?.ContributionRatio ?? ContributionRatio.Proportional,
            year, month);

        rules.Add(rule);
        return rule;
    }
}
