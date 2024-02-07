using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveExpenseCategory : CommandBase<int>
{
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public RemoveExpenseCategory(
        IUnitOfWork unitOfWork,
        IExpenseCategoryRepository expenseCategoryRepository) : base(unitOfWork)
    {
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expenseCategory = await expenseCategoryRepository.Get(id)
            ?? throw new Exception($"Expense category with ID '{id}' was not found.");

        if (expenseCategory.Expenses.Count > 0)
            throw new Exception($"Expense category with ID '{id}' can not be removed because it contains expenses.");

        expenseCategoryRepository.Remove(expenseCategory);
    }
}
