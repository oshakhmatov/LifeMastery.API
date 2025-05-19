using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public record FinanceContext(
    int Year,
    int Month,
    Expense[] Expenses,
    Expense[] FoodExpenses,
    ExpenseMonthDto[] ExpenseMonths,
    ExpenseCategory[] Categories,
    Currency[] Currencies,
    RegularPayment[] RegularPayments,
    EmailSubscription[] EmailSubscriptions,
    FamilyMember[] FamilyMembers,
    Earning[] Earnings,
    FamilyBudgetRule[] BudgetRules);
