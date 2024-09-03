namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class FinanceViewModel
{
    public required decimal MonthTotal { get; init; }
    public required int CurrentExpenseMonth { get; init; }
    public required int CurrentCurrency { get; init; }
    public required ExpenseMonthDto[] ExpenseMonths { get; init; }
    public required DailyExpensesDto[] DailyExpenses { get; init; }
    public required ExpenseCategoryDto[] ExpenseCategories { get; init; }
    public required CurrencyDto[] Currencies { get; init; }
    public required RegularPaymentDto[] RegularPayments { get; init; }
    public required EmailSubscriptionDto[] EmailSubscriptions { get; init; }
    public required ExpenseChartDto? ExpenseChart { get; init; }
    public required FinanceInfoDto? Info { get; init; }
    public required FinanceStatisticsDto Statistics { get; init; }
}
