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
        var expenses = await expenseRepository.List();
        var expenseCategories = await expenseCategoryRepository.List();
        var regularPayments = await regularPaymentRepository.List();
        var emailSubscriptions = await emailSubscriptionRepository.List(cancellationToken);
        var categorizedExpenses = expenses
            .Where(e => e.Category is not null)
            .GroupBy(e => e.Category!.Name)
            .OrderByDescending(g => g.Select(e => e.Amount).Sum());

        return new FinanceViewModel
        {
            CurrentMonthTotal = MathHelper.Round(expenses.Select(e => e.Amount).Sum()),
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
