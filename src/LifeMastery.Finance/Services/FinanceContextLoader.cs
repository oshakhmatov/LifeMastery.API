using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Commands;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using System.Data;

namespace LifeMastery.Finance.Services;

public class FinanceContextLoader(
        IExpenseRepository expenses,
        IExpenseCategoryRepository categories,
        IRegularPaymentRepository payments,
        IRepository<EmailSubscription> subscriptions,
        IRepository<Currency> currencies,
        IRepository<FamilyMember> familyMembers,
        IEarningRepository earnings,
        IFamilyBudgetRuleRepository rules)
{
    public async Task<FinanceContext> Load(GetFinanceData.Request request, CancellationToken token)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var year = request.Year ?? today.Year;
        var month = request.Month ?? today.Month;

        var allExpenses = await expenses.GetByPeriodAsync(year, month, token);

        return new FinanceContext(
            Year: year,
            Month: month,
            ExpenseMonths: await expenses.GetAvailableExpenseMonthsAsync(token),
            Expenses: allExpenses,
            Categories: await categories.GetAllOrderedByNameAsync(token),
            Currencies: await currencies.GetAllAsync(token),
            RegularPayments: await payments.GetAllOrderedByNewestAsync(token),
            EmailSubscriptions: await subscriptions.ListAsync(es => es.IsActive, token),
            FamilyMembers: await familyMembers.GetAllAsync(token),
            Earnings: await earnings.GetByPeriodAsync(year, month, token),
            BudgetRules: await rules.GetByPeriodAsync(year, month, token),
            FoodExpenses: allExpenses.Where(e => e.Category != null && e.Category.IsFood).ToArray());
    }
}