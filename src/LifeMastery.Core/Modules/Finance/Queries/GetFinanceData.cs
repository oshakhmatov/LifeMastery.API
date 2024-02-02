using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Queries;

public sealed class GetFinanceData
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IRegularPaymentRepository regularPaymentRepository;
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;

    public GetFinanceData(
        IExpenseRepository expenseRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        IRegularPaymentRepository regularPaymentRepository,
        IEmailSubscriptionRepository emailSubscriptionRepository)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
        this.regularPaymentRepository = regularPaymentRepository;
        this.emailSubscriptionRepository = emailSubscriptionRepository;
    }

    public async Task<FinanceViewModel> Execute(CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var prevMonth = today.AddMonths(-1).Month;
        var year = prevMonth == 12 ? today.AddYears(-1).Year : today.Year;

        var firstDayOfPrevMonth = new DateOnly(year, prevMonth, 1);
        var expenses = await expenseRepository.List(firstDayOfPrevMonth);
        var expenseCategories = await expenseCategoryRepository.List();
        var regularPayments = await regularPaymentRepository.List(cancellationToken);
        var emailSubscriptions = await emailSubscriptionRepository.List(cancellationToken);
        var prevMonthExpenses = expenses
            .Where(e => e.Date.Month == prevMonth)
            .ToList();
        var currentMonthExpenses = expenses
            .Where(e => e.Date.Month == today.Month)
            .ToList();
        var categorizedExpenses = currentMonthExpenses
            .Where(e => e.Category is not null)
            .GroupBy(e => e.Category!.Name)
            .OrderByDescending(g => g.Select(e => e.Amount).Sum());

        return new FinanceViewModel
        {
            PrevMonthTotal = MathHelper.Round(prevMonthExpenses.Select(e => e.Amount).Sum()),
            CurrentMonthTotal = MathHelper.Round(currentMonthExpenses.Select(e => e.Amount).Sum()),
            DailyExpenses = expenses.GroupBy(e => e.Date).Select(g => new DailyExpensesDto
            {
                Date = g.Key.ToString("dd.MM.yyyy"),
                Expenses = g.Select(ExpenseDto.FromModel).ToArray()

            }).ToArray(),
            ExpenseCategories = expenseCategories.Select(ExpenseCategoryDto.FromModel).ToArray(),
            RegularPayments = regularPayments.Select(RegularPaymentDto.FromModel).ToArray(),
            EmailSubscriptions = emailSubscriptions.Select(EmailSubscriptionDto.FromModel).ToArray(),
            ExpenseChart = new ExpenseChartDto
            {
                Labels = categorizedExpenses.Select(e => e.Key).ToArray(),
                Values = categorizedExpenses.Select(e => (long) e.Select(e => e.Amount).Sum()).ToArray(),
            }
        };
    }
}
