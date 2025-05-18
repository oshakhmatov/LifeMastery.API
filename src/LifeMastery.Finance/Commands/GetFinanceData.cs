using LifeMastery.Common;
using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Services;

namespace LifeMastery.Finance.Commands;

public sealed class GetFinanceData(
    FinanceContextLoader contextLoader,
    BudgetRuleResolver ruleResolver,
    EarningsResolver earningsResolver,
    FinanceStatisticsCalculator statisticsCalculator,
    IUnitOfWork unitOfWork) : ICommand<GetFinanceDataRequest, FinanceViewModel>
{
    public async Task<FinanceViewModel> Execute(GetFinanceDataRequest request, CancellationToken token)
    {
        var context = await contextLoader.Load(request, token);
        var budgetRule = await ruleResolver.Resolve(context.BudgetRules, context.Year, context.Month, token);
        context.Earnings = await earningsResolver.Resolve(context.Earnings, context.FamilyMembers, context.Year, context.Month, token);
        await unitOfWork.Commit(token);

        var expenses = context.Expenses;
        var categorizedExpenses = expenses
            .Where(e => e.Category is not null)
            .GroupBy(e => e.Category!.Name)
            .OrderByDescending(g => g.Sum(e => e.Amount));

        var periodPayments = context.RegularPayments
            .SelectMany(rp => rp.Payments)
            .Where(p => p.PeriodYear == context.Year && p.PeriodMonth == context.Month)
            .ToArray();

        var taxPayments = context.RegularPayments
            .Where(rp => rp.IsTax)
            .SelectMany(rp => rp.Payments)
            .Where(p => p.PeriodYear == context.Year && p.PeriodMonth == context.Month)
            .ToArray();

        var statistics = statisticsCalculator.Calculate(
            context.Info?.Income,
            context.Expenses,
            context.FoodExpenses,
            taxPayments
        );

        var contributionChart = GetFinanceDataHelper.BuildContributionChart(budgetRule.ContributionRatio, context.Earnings);
        var familyBudgetStats = GetFinanceDataHelper.BuildFamilyBudgetStats(
            context.Earnings,
            context.Expenses,
            budgetRule.ContributionRatio,
            periodPayments
        );

        return new FinanceViewModel
        {
            MonthTotal = MathHelper.Round(expenses.Sum(e => e.Amount)),
            DailyExpenses = expenses
                .GroupBy(e => e.Date)
                .Select(g => new DailyExpensesDto
                {
                    Date = g.Key.ToString("dd.MM.yyyy"),
                    Expenses = g.Select(ExpenseDto.FromModel).ToArray()
                })
                .ToArray(),
            Currencies = context.Currencies.Select(CurrencyDto.FromModel).ToArray(),
            FamilyMembers = context.FamilyMembers.Select(m => m.ToDto()).ToArray(),
            Earnings = context.Earnings.Select(e => e.ToDto()).ToArray(),
            FamilyBudgetRule = budgetRule.ToDto(),
            AvailableContributionRatios = ["Поровну", "Пропорционально"],
            FamilyMemberBudgetStats = familyBudgetStats,
            ExpenseCategories = context.Categories.Select(ExpenseCategoryDto.FromModel).ToArray(),
            RegularPayments = context.RegularPayments
                .Select(rp => rp.ToDto())
                .OrderBy(rp => rp.IsPaid)
                .ThenBy(rp => rp.Name)
                .ToArray(),
            EmailSubscriptions = context.EmailSubscriptions.Select(es => es.ToDto()).ToArray(),
            ExpenseChart = new ChartDto
            {
                Labels = categorizedExpenses.Select(g => g.Key).ToArray(),
                Values = categorizedExpenses.Select(g => (long)g.Sum(e => e.Amount)).ToArray(),
                Colors = categorizedExpenses.Select(g => g.First().Category!.Color).ToArray()
            },
            ContributionChart = contributionChart,
            CurrentCurrency = 0,
            CurrentExpenseMonth = Array.IndexOf(context.ExpenseMonths, context.ExpenseMonths.FirstOrDefault(m => m.Year == context.Year && m.Month == context.Month)),
            ExpenseMonths = context.ExpenseMonths,
            Info = new FinanceInfoDto { Income = context.Info?.Income },
            Statistics = statistics
        };
    }
}

public sealed class GetFinanceDataRequest
{
    public int? CurrencyId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
}
