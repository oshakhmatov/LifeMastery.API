namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class FinanceViewModel
{
    public ExpenseDto[] Expenses { get; set; }
    public ExpenseCategoryDto[] ExpenseCategories { get; set; }
    public RegularPaymentDto[] RegularPayments { get; set; }
}
