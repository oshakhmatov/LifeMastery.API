using LifeMastery.Common;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Services;

public class FinanceStatisticsCalculator
{
    public FinanceStatisticsDto? Calculate(decimal income, Expense[] expenses, Expense[] food, Payment[] payments, Payment[] taxPayments)
    {
        if (income == 0 || (expenses.Length == 0 && payments.Length == 0))
            return null;

        return new FinanceStatisticsDto(
            RemainingAmountPercent: CalcRemaining(income, expenses, payments),
            FoodSpendingPercent: CalcPercent(income, [.. food.Select(f => f.Amount)]),
            OverallTaxPercent: CalcPercent(income, [.. taxPayments.Select(t => t.Amount)]));
        
    }

    private static decimal? CalcRemaining(decimal income, Expense[] expenses, Payment[] taxes)
    {
        var totalExpenses = expenses.Sum(e => e.Amount);
        var totalTaxes = taxes.Sum(p => p.Amount);
        var totalSpending = totalExpenses + totalTaxes;
        
        return 100 - MathHelper.Round(totalSpending / income * 100);
    }

    private static decimal? CalcPercent(decimal income, decimal[] values)
    {
        if (values.Length == 0)
            return null;

        return MathHelper.Round(values.Sum() / income * 100);
    }
}
