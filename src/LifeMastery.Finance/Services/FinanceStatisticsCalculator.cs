using LifeMastery.Common;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Services;

public class FinanceStatisticsCalculator
{
    public FinanceStatisticsDto? Calculate(decimal income, Expense[] expenses, Expense[] food, Payment[] taxes)
    {
        if (income == 0 || (expenses.Length == 0 && taxes.Length == 0))
            return null;

        return new FinanceStatisticsDto(
            RemainingAmountPercent: CalcRemaining(income, expenses, taxes),
            FoodSpendingPercent: CalcPercent(income, [.. food.Select(f => f.Amount)]),
            OverallTaxPercent: CalcPercent(income, [.. taxes.Select(t => t.Amount)]));
        
    }

    private static decimal? CalcRemaining(decimal income, Expense[] expenses, Payment[] taxes)
    {
        var total = expenses.Sum(e => e.Amount) + taxes.Sum(p => p.Amount);
        return 100 - MathHelper.Round(total / income * 100);
    }

    private static decimal? CalcPercent(decimal income, decimal[] values)
    {
        if (values.Length != 0)
            return null;

        return MathHelper.Round(values.Sum() / income * 100);
    }
}
