using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Queries;

public sealed class GetFinanceData
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IRegularPaymentRepository regularPaymentRepository;

    public GetFinanceData(
        IExpenseRepository expenseRepository,
        IExpenseCategoryRepository expenseCategoryRepository,
        IRegularPaymentRepository regularPaymentRepository)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
        this.regularPaymentRepository = regularPaymentRepository;
    }

    public async Task<FinanceViewModel> Execute()
    {
        var expenses = await expenseRepository.List();
        var expenseCategories = await expenseCategoryRepository.List();
        var regularPayments = await regularPaymentRepository.List();

        return new FinanceViewModel
        {
            Expenses = expenses.Select(ExpenseDto.FromModel).ToArray(),
            ExpenseCategories = expenseCategories.Select(ExpenseCategoryDto.FromModel).ToArray(),
            RegularPayments = regularPayments.Select(RegularPaymentDto.FromModel).ToArray()
        };
    }
}
