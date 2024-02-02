using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Repositories;

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

    public async Task<FinanceViewModel> Execute(GetFinanceDataRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var year = request.Year ?? today.Year;
        var month = request.Month ?? today.Month;

        var expenseMonths = await expenseRepository.GetExpenseMonths();
        var expenses = await expenseRepository.List(year, month);
        var expenseCategories = await expenseCategoryRepository.List();
        var regularPayments = await regularPaymentRepository.List(cancellationToken);
        var emailSubscriptions = await emailSubscriptionRepository.List(cancellationToken);

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
            RegularPayments = regularPayments.Select(RegularPaymentDto.FromModel).ToArray(),
            EmailSubscriptions = emailSubscriptions.Select(EmailSubscriptionDto.FromModel).ToArray(),
            ExpenseChart = new ExpenseChartDto
            {
                Labels = categorizedExpenses.Select(e => e.Key).ToArray(),
                Values = categorizedExpenses.Select(e => (long) e.Select(e => e.Amount).Sum()).ToArray(),
            },
            CurrentExpenseMonth = Array.IndexOf(expenseMonths, expenseMonths.FirstOrDefault(e => e.Year == year && e.Month == month)),
            ExpenseMonths = expenseMonths.Select(e => new ExpenseMonthDto
            {
                Month = e.Month,
                Year = e.Year,
                Name = $"{DateHelper.GetMonthName(e.Month)} {e.Year}"
            }).ToArray(),
        };
    }
}
