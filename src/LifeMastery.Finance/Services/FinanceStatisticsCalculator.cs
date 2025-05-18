using LifeMastery.Common;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Services;

public class FinanceStatisticsCalculator
{
    public FinanceStatisticsDto Calculate(decimal? income, Expense[] expenses, Expense[] food, Payment[] taxes)
    {
        return new FinanceStatisticsDto
        {
            RemainingAmountPercent = CalcRemaining(income, expenses, taxes),
            FoodSpendingPercent = CalcPercent(income, food.Select(f => f.Amount)),
            OverallTaxPercent = CalcPercent(income, taxes.Select(t => t.Amount))
        };
    }

    private static decimal? CalcRemaining(decimal? income, Expense[] expenses, Payment[] payments)
    {
        if (income == null || (expenses.Length == 0 && payments.Length == 0))
            return null;

        var total = expenses.Sum(e => e.Amount) + payments.Sum(p => p.Amount);
        return 100 - MathHelper.Round(total / income.Value * 100);
    }

    private static decimal? CalcPercent(decimal? income, IEnumerable<decimal> values)
    {
        if (income == null || !values.Any()) return null;
        return MathHelper.Round(values.Sum() / income.Value * 100);
    }
}
