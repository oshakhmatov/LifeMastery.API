namespace LifeMastery.Finance.DataTransferObjects;

public sealed class FinanceViewModel
{
    public required decimal MonthTotal { get; init; }
    public required int CurrentExpenseMonth { get; init; }
    public required int CurrentCurrency { get; init; }
    public required ExpenseMonthDto[] ExpenseMonths { get; init; }
    public required DailyExpensesDto[] DailyExpenses { get; init; }
    public required ExpenseCategoryDto[] ExpenseCategories { get; init; }
    public required CurrencyDto[] Currencies { get; init; }
    public required FamilyMemberDto[] FamilyMembers { get; init; }
    public required EarningDto[] Earnings { get; init; }
    public required FamilyBudgetRuleDto FamilyBudgetRule { get; init; }
    public required ChartDto? ContributionChart { get; init; }
    public required string[] AvailableContributionRatios { get; init; }
    public required RegularPaymentDto[] RegularPayments { get; init; }
    public required EmailSubscriptionDto[] EmailSubscriptions { get; init; }
    public required ChartDto? ExpenseChart { get; init; }
    public required FinanceInfoDto? Info { get; init; }
    public required FinanceStatisticsDto Statistics { get; init; }
    public required FamilyMemberBudgetDto[] FamilyMemberBudgetStats { get; init; }
}
