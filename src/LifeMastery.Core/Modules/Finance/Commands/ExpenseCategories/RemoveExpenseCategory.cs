using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.ExpenseCategories;

public sealed class RemoveExpenseCategory(
    IUnitOfWork unitOfWork,
    IExpenseCategoryRepository expenseCategoryRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expenseCategory = await expenseCategoryRepository.Get(id, token)
            ?? throw new Exception($"Expense category with ID '{id}' was not found.");

        if (expenseCategory.Expenses.Count > 0)
            throw new Exception($"Expense category with ID '{id}' can not be removed because it contains expenses.");

        expenseCategoryRepository.Remove(expenseCategory);
    }
}
