using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveExpenseCategory
{
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IUnitOfWork unitOfWork;

    public RemoveExpenseCategory(IExpenseCategoryRepository expenseCategoryRepository, IUnitOfWork unitOfWork)
    {
        this.expenseCategoryRepository = expenseCategoryRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var expenseCategory = await expenseCategoryRepository.Get(id);
        if (expenseCategory is null)
            throw new Exception($"Expense category with ID '{id}' was not found.");

        if (expenseCategory.Expenses.Any())
            throw new Exception($"Expense category with ID '{id}' can not be removed because it contains expenses.");

        expenseCategoryRepository.Remove(expenseCategory);

        await unitOfWork.Commit();
    }
}
