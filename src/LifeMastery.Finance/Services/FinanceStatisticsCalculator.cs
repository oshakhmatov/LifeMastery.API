using LifeMastery.Common;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Services;

public class FinanceStatisticsCalculator
{
    public FinanceStatisticsDto? Calculate(decimal income, Expense[] expenses, Expense[] food, Payment[] payments, Payment[] taxPayments, RegularPayment[] regularPayments)
    {
   if (income == 0 || (expenses.Length == 0 && payments.Length == 0))
          return null;

    var items = BuildFinanceItems(expenses, regularPayments);

   return new FinanceStatisticsDto(
    RemainingAmountPercent: CalcRemaining(income, expenses, payments),
         FoodSpendingPercent: CalcPercent(income, [.. food.Select(f => f.Amount)]),
         OverallTaxPercent: CalcPercent(income, [.. taxPayments.Select(t => t.Amount)]),
         Items: items);
        
    }

    private static FinanceItemDto[] BuildFinanceItems(Expense[] expenses, RegularPayment[] regularPayments)
    {
        var items = new List<FinanceItemDto>();

        // Add payments - with yearly payments converted to monthly average
        foreach (var regularPayment in regularPayments.Where(rp => rp.IsActive))
        {
 var payments = regularPayment.Payments.ToArray();
  if (payments.Length == 0)
     continue;

          decimal amount;
            if (regularPayment.Period == Enums.Period.Year)
      {
 // Calculate monthly average for yearly payments
      amount = MathHelper.Round(payments.Sum(p => p.Amount) / 12);
            }
   else
   {
           // For monthly payments, take the latest payment amount
         var latestPayment = payments.OrderByDescending(p => p.PeriodYear).ThenByDescending(p => p.PeriodMonth).First();
          amount = MathHelper.Round(latestPayment.Amount);
      }

        items.Add(new FinanceItemDto(regularPayment.Name, amount));
        }

        // Add expenses grouped by category
        var categorizedExpenses = expenses
   .Where(e => e.Category is not null)
            .GroupBy(e => e.Category!.Name)
            .Select(g => new FinanceItemDto(
                g.Key,
                MathHelper.Round(g.Sum(e => e.Amount))))
            .OrderByDescending(item => item.Amount);

      items.AddRange(categorizedExpenses);

        return items.OrderByDescending(i => i.Amount).ToArray();
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
