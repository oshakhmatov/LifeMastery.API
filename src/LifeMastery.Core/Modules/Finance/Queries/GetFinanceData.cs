using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Queries;

public sealed class GetFinanceDataRequest
{
    public int? CurrencyId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
}

public sealed class GetFinanceData(
    IExpenseRepository expenseRepository,
    IExpenseCategoryRepository expenseCategoryRepository,
    IRegularPaymentRepository regularPaymentRepository,
    IEmailSubscriptionRepository emailSubscriptionRepository,
    IFinanceInfoRepository financeInfoRepository,
    ICurrencyRepository currencyRepository,
    IFamilyMemberRepository familyMemberRepository,
    IEarningRepository earningRepository,
    IFamilyBudgetRuleRepository familyBudgetRuleRepository,
    IUnitOfWork unitOfWork)
{
    private static readonly string[] PredefinedColors = ["#4e79a7", "#f28e2c", "#e15759", "#76b7b2", "#59a14f"];

    public async Task<FinanceViewModel> Execute(GetFinanceDataRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var year = request.Year ?? today.Year;
        var month = request.Month ?? today.Month;

        var expenseMonths = await expenseRepository.GetExpenseMonths(cancellationToken);
        var expenses = await expenseRepository.List(year, month, cancellationToken);
        var expenseCategories = await expenseCategoryRepository.List(cancellationToken);
        var currencies = await currencyRepository.List(cancellationToken);
        var regularPayments = await regularPaymentRepository.List(cancellationToken);
        var emailSubscriptions = await emailSubscriptionRepository.List(cancellationToken);
        var info = await financeInfoRepository.Get(cancellationToken);
        var familyMembers = await familyMemberRepository.List(cancellationToken);
        var earnings = await earningRepository.List(year, month, cancellationToken);
        var familyBudgetRules = await familyBudgetRuleRepository.List(year, month, cancellationToken);

        //if (request.CurrencyId != null) { 
        //}

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

        var selectedFamilyBudgetRule = familyBudgetRules
            .Where(fbr => fbr.PeriodYear == year && fbr.PeriodMonth == month)
            .FirstOrDefault();

        if (selectedFamilyBudgetRule == null)
        {
            var latestFamilyBudgetRule = await familyBudgetRuleRepository.GetLatest(cancellationToken);

            selectedFamilyBudgetRule = new FamilyBudgetRule(
                latestFamilyBudgetRule?.ContributionRatio ?? ContributionRatio.Proportional,
                periodYear: year,
                periodMonth: month);

            familyBudgetRuleRepository.Put(selectedFamilyBudgetRule);
        }

        foreach (var familyMember in familyMembers)
        {
            var earning = earnings
                .Where(e => e.FamilyMember == familyMember)
                .FirstOrDefault();

            if (earning == null)
            {
                var latestEarning = await earningRepository.GetLatestByFamilyMember(familyMember, cancellationToken);

                earning = new Earning(
                    amount: latestEarning?.Amount ?? 0, 
                    periodYear: year,
                    periodMonth: month,
                    familyMember: familyMember);

                earningRepository.Put(earning);
            }
        }

        await unitOfWork.Commit(cancellationToken);
        earnings = await earningRepository.List(year, month, cancellationToken);
        familyBudgetRules = await familyBudgetRuleRepository.List(year, month, cancellationToken);
        selectedFamilyBudgetRule = familyBudgetRules
            .Where(fbr => fbr.PeriodYear == year && fbr.PeriodMonth == month)
            .First();

        var contributionChart = BuildContributionChart(selectedFamilyBudgetRule.ContributionRatio, earnings);
        var familyBudgetStats = BuildFamilyBudgetStats(earnings, expenses, selectedFamilyBudgetRule.ContributionRatio);

        return new FinanceViewModel
        {
            MonthTotal = MathHelper.Round(expenses.Select(e => e.Amount).Sum()),
            DailyExpenses = expenses.GroupBy(e => e.Date).Select(g => new DailyExpensesDto
            {
                Date = g.Key.ToString("dd.MM.yyyy"),
                Expenses = g.Select(ExpenseDto.FromModel).ToArray()

            }).ToArray(),
            Currencies = currencies.Select(CurrencyDto.FromModel).ToArray(),
            FamilyMembers = familyMembers.Select(fm => fm.ToDto()).ToArray(),
            Earnings = earnings.Select(e => e.ToDto()).ToArray(),
            FamilyBudgetRule = selectedFamilyBudgetRule.ToDto(),
            AvailableContributionRatios = [ "Поровну", "Пропорционально" ],
            FamilyMemberBudgetStats = familyBudgetStats,
            ExpenseCategories = expenseCategories.Select(ExpenseCategoryDto.FromModel).ToArray(),
            RegularPayments = regularPayments
                .Select(rp => rp.ToDto())
                .OrderBy(rp => rp.IsPaid)
                .ThenBy(rp => rp.Name)
                .ToArray(),
            EmailSubscriptions = emailSubscriptions.Select(es => es.ToDto()).ToArray(),
            ExpenseChart = new ChartDto
            {
                Labels = categorizedExpenses.Select(e => e.Key).ToArray(),
                Values = categorizedExpenses.Select(e => (long) e.Select(e => e.Amount).Sum()).ToArray(),
                Colors = categorizedExpenses.Select(e => e.Select(e => e.Category!).First().Color).ToArray()
            },
            ContributionChart = contributionChart,
            CurrentCurrency = 0,
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

    private static decimal? GetRemainingAmountPercent(decimal? income, Expense[] expenses, Payment[] payments)
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

    public static ChartDto? BuildContributionChart(
        ContributionRatio contributionRatio,
        IEnumerable<Earning> earnings)
    {
        var shares = CalculateContributionShares(contributionRatio, earnings);

        var labels = shares.Keys.Select(m => m.Name).ToArray();
        var colors = labels
            .Select((_, i) => PredefinedColors[i % PredefinedColors.Length])
            .ToArray();

        var rawValues = shares.Values.ToArray();
        var floored = rawValues.Select(x => (long)Math.Floor(x)).ToArray();
        var diff = 100L - floored.Sum();
        for (int i = 0; i < diff; i++)
            floored[i % floored.Length]++;

        return new ChartDto
        {
            Labels = labels,
            Values = floored,
            Colors = colors
        };
    }

    public static FamilyMemberBudgetDto[] BuildFamilyBudgetStats(
        IEnumerable<Earning> earnings,
        IEnumerable<Expense> expenses,
        ContributionRatio contributionRatio)
    {
        var earningsByMember = earnings
            .GroupBy(e => e.FamilyMember)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        var personalExpensesByMember = expenses
            .Where(e => e.Category?.FamilyMember != null)
            .GroupBy(e => e.Category!.FamilyMember!)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        var sharedTotal = expenses
            .Where(e => e.Category?.FamilyMember == null)
            .Sum(e => e.Amount);

        var contributionShares = CalculateContributionShares(contributionRatio, earnings);

        var sharedByMember = contributionShares
            .ToDictionary(
                kvp => kvp.Key,
                kvp => Math.Round(sharedTotal * (kvp.Value / 100m), 2)
            );

        var result = new List<FamilyMemberBudgetDto>();

        foreach (var member in earningsByMember.Keys)
        {
            var income = earningsByMember[member];
            var personal = personalExpensesByMember.GetValueOrDefault(member, 0m);
            var shared = sharedByMember.GetValueOrDefault(member, 0m);
            var savings = income - personal - shared;

            result.Add(new FamilyMemberBudgetDto
            {
                FamilyMemberName = member.Name,
                NetSavings = savings,
                PersonalExpenses = personal,
                SharedContribution = shared
            });
        }

        return result.ToArray();
    }


    public static Dictionary<FamilyMember, decimal> CalculateContributionShares(
        ContributionRatio contributionRatio,
        IEnumerable<Earning> earnings)
    {
        var earningsByMember = earnings
            .GroupBy(e => e.FamilyMember)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        if (contributionRatio == ContributionRatio.Equal)
        {
            var count = earningsByMember.Count;
            var percent = 100m / count;

            return earningsByMember.Keys
                .ToDictionary(m => m, _ => percent);
        }
        else if (contributionRatio == ContributionRatio.Proportional)
        {
            var total = earningsByMember.Values.Sum();
            var raw = earningsByMember
                .ToDictionary(kvp => kvp.Key, kvp => (kvp.Value / total) * 100m);

            return raw;
        }

        throw new InvalidOperationException("Unsupported contribution ratio");
    }
}
