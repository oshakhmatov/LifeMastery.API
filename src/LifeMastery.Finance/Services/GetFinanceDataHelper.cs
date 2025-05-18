using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Enums;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Services;

public static class GetFinanceDataHelper
{
    private static readonly string[] PredefinedColors =
        ["#4e79a7", "#f28e2c", "#e15759", "#76b7b2", "#59a14f"];

    public static ChartDto BuildContributionChart(
        ContributionRatio ratio,
        IEnumerable<Earning> earnings)
    {
        var shares = CalculateContributionShares(ratio, earnings);

        var labels = shares.Keys.Select(m => m.Name).ToArray();
        var colors = labels.Select((_, i) => PredefinedColors[i % PredefinedColors.Length]).ToArray();

        var rawValues = shares.Values.ToArray();
        var floored = rawValues.Select(x => (long)Math.Floor(x)).ToArray();
        var diff = 100L - floored.Sum();
        for (int i = 0; i < diff; i++)
            floored[i % floored.Length]++;

        return new ChartDto
        {
            Labels = labels,
            Values = floored,
            Colors = colors
        };
    }

    public static FamilyMemberBudgetDto[] BuildFamilyBudgetStats(
        IEnumerable<Earning> earnings,
        IEnumerable<Expense> expenses,
        ContributionRatio ratio,
        Payment[] payments)
    {
        var earningsByMember = earnings
            .GroupBy(e => e.FamilyMember)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        if (earningsByMember.Count == 0)
            return [];

        var personalExpensesByMember = expenses
            .Where(e => e.Category?.FamilyMember != null)
            .GroupBy(e => e.Category!.FamilyMember!)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        var sharedExpenses = expenses.Where(e => e.Category?.FamilyMember == null).Sum(e => e.Amount);
        var sharedPayments = payments.Sum(p => p.Amount);
        var sharedTotal = sharedExpenses + sharedPayments;

        var shares = CalculateContributionShares(ratio, earnings);

        var sharedByMember = shares.ToDictionary(
            kvp => kvp.Key,
            kvp => Math.Round(sharedTotal * (kvp.Value / 100m), 2));

        return earningsByMember.Keys.Select(member =>
        {
            var income = earningsByMember[member];
            var personal = personalExpensesByMember.GetValueOrDefault(member, 0);
            var shared = sharedByMember.GetValueOrDefault(member, 0);
            return new FamilyMemberBudgetDto
            {
                FamilyMemberName = member.Name,
                NetSavings = income - personal - shared,
                PersonalExpenses = personal,
                SharedContribution = shared
            };
        }).ToArray();
    }

    public static Dictionary<FamilyMember, decimal> CalculateContributionShares(
        ContributionRatio ratio,
        IEnumerable<Earning> earnings)
    {
        var grouped = earnings
            .GroupBy(e => e.FamilyMember)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        if (grouped.Count == 0)
            throw new InvalidOperationException("Cannot calculate shares: no earnings.");

        return ratio switch
        {
            ContributionRatio.Equal => grouped.Keys
                .ToDictionary(m => m, _ => 100m / grouped.Count),

            ContributionRatio.Proportional => grouped.Values.Sum() == 0
                ? grouped.ToDictionary(kvp => kvp.Key, _ => 0m)
                : grouped.ToDictionary(kvp => kvp.Key, kvp => (kvp.Value / grouped.Values.Sum()) * 100m),

            _ => throw new InvalidOperationException($"Unsupported ratio: {ratio}")
        };
    }
}
