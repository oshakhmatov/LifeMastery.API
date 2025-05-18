using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public class FinanceContext
{
    public int Year { get; set; }
    public int Month { get; set; }
    public Expense[] Expenses { get; set; } = [];
    public Expense[] FoodExpenses { get; set; } = [];
    public ExpenseMonthDto[] ExpenseMonths { get; set; } = [];
    public ExpenseCategory[] Categories { get; set; } = [];
    public Currency[] Currencies { get; set; } = [];
    public RegularPayment[] RegularPayments { get; set; } = [];
    public EmailSubscription[] EmailSubscriptions { get; set; } = [];
    public FinanceInfo? Info { get; set; }
    public FamilyMember[] FamilyMembers { get; set; } = [];
    public Earning[] Earnings { get; set; } = [];
    public FamilyBudgetRule[] BudgetRules { get; set; } = [];
}