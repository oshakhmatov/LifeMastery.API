namespace LifeMastery.Finance.DataTransferObjects;

public record FinanceViewModel(
    decimal MonthTotal,
    int CurrentExpenseMonth,
    int CurrentCurrency,
    ExpenseMonthDto[] ExpenseMonths,
    DailyExpensesDto[] DailyExpenses,
    ExpenseCategoryDto[] ExpenseCategories,
    CurrencyDto[] Currencies,
    FamilyMemberDto[] FamilyMembers,
    EarningDto[] Earnings,
    FamilyBudgetRuleDto FamilyBudgetRule,
    ChartDto? ContributionChart,
    string[] AvailableContributionRatios,
    RegularPaymentDto[] RegularPayments,
    EmailSubscriptionDto[] EmailSubscriptions,
    ChartDto? ExpenseChart,
    FinanceStatisticsDto Statistics,
    FamilyMemberBudgetDto[] FamilyMemberBudgetStats);
