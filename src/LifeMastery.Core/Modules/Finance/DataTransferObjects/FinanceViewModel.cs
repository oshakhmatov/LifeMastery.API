namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class FinanceViewModel
{
    public decimal CurrentMonthTotal { get; set; }
    public DailyExpensesDto[] DailyExpenses { get; set; }
    public ExpenseCategoryDto[] ExpenseCategories { get; set; }
    public RegularPaymentDto[] RegularPayments { get; set; }
    public EmailSubscriptionDto[] EmailSubscriptions { get; set; }
}
