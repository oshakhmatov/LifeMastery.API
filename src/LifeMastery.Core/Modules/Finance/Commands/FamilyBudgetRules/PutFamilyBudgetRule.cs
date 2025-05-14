using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Currencies;

public class PutFamilyBudgetRuleRequest
{
    public int Id { get; init; }
    public required string ContributionRatio { get; init; }
}

public class PutFamilyBudgetRule(
    IFamilyBudgetRuleRepository familyBudgetRuleRepository,
    IUnitOfWork unitOfWork) : CommandBase<PutFamilyBudgetRuleRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutFamilyBudgetRuleRequest command, CancellationToken token = default)
    {
        var familyBudgetRule = await familyBudgetRuleRepository.Get(command.Id, token)
                ?? throw new ApplicationException($"FamilyBudgetRule with ID '{command.Id}' was not found.");

        familyBudgetRule.ContributionRatio = GetContributionRatioType(command.ContributionRatio);
    }

    private ContributionRatio GetContributionRatioType(string contributionRatio)
    {
        return contributionRatio switch
        {
            "Поровну" => ContributionRatio.Equal,
            "Пропорционально" => ContributionRatio.Proportional,
            _ => throw new ArgumentException($"Invalid contribution ratio type: {contributionRatio}")
        };
    }
}