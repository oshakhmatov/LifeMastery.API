using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Enums;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.FamilyBudgetRules;

public sealed class UpdateFamilyBudgetRule(
    IRepository<FamilyBudgetRule> rules,
    IUnitOfWork unitOfWork) : ICommand<UpdateFamilyBudgetRule.Request>
{
    public async Task Execute(Request command, CancellationToken token)
    {
        var rule = await rules.GetByIdAsync(command.Id, token)
            ?? throw new AppException($"FamilyBudgetRule with ID '{command.Id}' was not found.");

        rule.ContributionRatio = ParseRatio(command.ContributionRatio);
        await unitOfWork.Commit(token);
    }

    private static ContributionRatio ParseRatio(string input) => input switch
    {
        "Поровну" => ContributionRatio.Equal,
        "Пропорционально" => ContributionRatio.Proportional,
        _ => throw new AppException($"Invalid contribution ratio: {input}")
    };

    public record Request(int Id, string ContributionRatio);
}
