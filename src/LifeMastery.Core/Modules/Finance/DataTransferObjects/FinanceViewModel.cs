namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class FinanceViewModel
{
    public decimal MonthTotal { get; set; }
    public int CurrentExpenseMonth { get; set; }
    public ExpenseMonthDto[] ExpenseMonths { get; set; }
    public DailyExpensesDto[] DailyExpenses { get; set; }
    public ExpenseCategoryDto[] ExpenseCategories { get; set; }
    public RegularPaymentDto[] RegularPayments { get; set; }
    public EmailSubscriptionDto[] EmailSubscriptions { get; set; }
    public ExpenseChartDto? ExpenseChart { get; set; }
}
