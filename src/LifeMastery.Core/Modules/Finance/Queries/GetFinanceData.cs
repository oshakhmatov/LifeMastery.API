using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using System;

namespace LifeMastery.Core.Modules.Finance.Queries;

public sealed class GetFinanceDataRequest
{
    public int? Year { get; set; }
    public int? Month { get; set; }
}

public sealed class GetFinanceData
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IRegularPaymentRepository regularPaymentRepository;
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;
    private readonly IFinanceInfoRepository financeInfoRepository;

    public GetFinanceData(
        IExpenseRepository expenseRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        IRegularPaymentRepository regularPaymentRepository,
        IEmailSubscriptionRepository emailSubscriptionRepository,
        IFinanceInfoRepository financeInfoRepository)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
        this.regularPaymentRepository = regularPaymentRepository;
        this.emailSubscriptionRepository = emailSubscriptionRepository;
        this.financeInfoRepository = financeInfoRepository;
    }

    public async Task<FinanceViewModel> Execute(GetFinanceDataRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var year = request.Year ?? today.Year;
        var month = request.Month ?? today.Month;

        var expenseMonths = await expenseRepository.GetExpenseMonths(cancellationToken);
        var expenses = await expenseRepository.List(year, month, cancellationToken);
        var expenseCategories = await expenseCategoryRepository.List(cancellationToken);
        var regularPayments = await regularPaymentRepository.List(cancellationToken);
        var emailSubscriptions = await emailSubscriptionRepository.List(cancellationToken);
        var info = await financeInfoRepository.Get(cancellationToken);

        var taxRegularPayments = regularPayments
            .Where(p => p.IsTax)
            .ToArray();

        var foodExpenses = expenses
            .Where(e => e.Category != null && e.Category!.IsFood)
            .ToArray();

        var taxPayments = regularPayments
            .Where(rp => rp.IsTax)
            .SelectMany(rp => rp.Payments)
            .Where(p => p.PeriodYear == year && p.PeriodMonth == month)
            .ToArray();

        var periodPayments = regularPayments
            .SelectMany(rp => rp.Payments)
            .Where(p => p.PeriodYear == year && p.PeriodMonth == month)
            .ToArray();

        var categorizedExpenses = expenses
            .Where(e => e.Category is not null)
            .GroupBy(e => e.Category!.Name)
            .OrderByDescending(g => g.Select(e => e.Amount).Sum());

        return new FinanceViewModel
        {
            MonthTotal = MathHelper.Round(expenses.Select(e => e.Amount).Sum()),
            DailyExpenses = expenses.GroupBy(e => e.Date).Select(g => new DailyExpensesDto
            {
                Date = g.Key.ToString("dd.MM.yyyy"),
                Expenses = g.Select(ExpenseDto.FromModel).ToArray()

            }).ToArray(),
            ExpenseCategories = expenseCategories.Select(ExpenseCategoryDto.FromModel).ToArray(),
            RegularPayments = regularPayments.Select(rp => rp.ToDto()).ToArray(),
            EmailSubscriptions = emailSubscriptions.Select(EmailSubscriptionDto.FromModel).ToArray(),
            ExpenseChart = new ExpenseChartDto
            {
                Labels = categorizedExpenses.Select(e => e.Key).ToArray(),
                Values = categorizedExpenses.Select(e => (long) e.Select(e => e.Amount).Sum()).ToArray(),
                Colors = categorizedExpenses.Select(e => e.Select(e => e.Category).First().Color).ToArray()
            },
            CurrentExpenseMonth = Array.IndexOf(expenseMonths, expenseMonths.FirstOrDefault(e => e.Year == year && e.Month == month)),
            ExpenseMonths = expenseMonths.Select(e => new ExpenseMonthDto
            {
                Month = e.Month,
                Year = e.Year,
                Name = $"{DateHelper.GetMonthName(e.Month)} {e.Year}"
            }).ToArray(),
            Info = new FinanceInfoDto
            {
                Income = info?.Income
            },
            Statistics = new FinanceStatisticsDto
            {
                RemainingAmountPercent = GetRemainingAmountPercent(info?.Income, expenses, periodPayments),
                FoodSpendingPercent = GetFoodSpendingPercent(info?.Income, foodExpenses),
                OverallTaxPercent = GetOverallTaxPercent(info?.Income, taxPayments)
            }
        };
    }

    private decimal? GetRemainingAmountPercent(decimal? income, Expense[] expenses, Payment[] payments)
    {
        if (income == null)
        {
            return null;
        }

        if (expenses.Length == 0 && payments.Length == 0)
        {
            return null;
        }

        var expensesSum = expenses.Sum(e => e.Amount);
        var paymentsSum = payments.Sum(e => e.Amount);
        var sum = expensesSum + paymentsSum;

        var sumPercent = sum / income.Value * 100;

        return 100 - MathHelper.Round(sumPercent);
    }

    private static decimal? GetFoodSpendingPercent(decimal? income, Expense[] foodExpenses)
    {
        if (income == null)
        {
            return null;
        }

        if (foodExpenses.Length == 0)
        {
            return null;
        }

        var foodSpending = foodExpenses.Sum(e => e.Amount);
        var foodSpendingPercent = foodSpending / income.Value * 100;

        return MathHelper.Round(foodSpendingPercent);
    }

    private static decimal? GetOverallTaxPercent(decimal? income, Payment[] taxPayments)
    {
        if (income == null)
        {
            return null;
        }

        if (taxPayments.Length == 0)
        {
            return null;
        }

        var overallTax = taxPayments.Sum(e => e.Amount);
        var overallTaxPercent = overallTax / income.Value * 100;

        return MathHelper.Round(overallTaxPercent);
    }
}
