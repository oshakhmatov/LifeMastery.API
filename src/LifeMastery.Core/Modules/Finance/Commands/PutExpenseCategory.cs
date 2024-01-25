using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutExpenseCategoryRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
}

public sealed class PutExpenseCategory
{
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IUnitOfWork unitOfWork;

    public PutExpenseCategory(
        IUnitOfWork unitOfWork,
        IExpenseCategoryRepository expenseCategoryRepository)
    {
        this.unitOfWork = unitOfWork;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task Execute(PutExpenseCategoryRequest request)
    {
        var existingCategory = await expenseCategoryRepository.GetByName(request.Name);
        if (existingCategory is not null)
            throw new Exception($"Expense category with name '{request.Name}' already exists.");

        if (request.Id.HasValue)
        {
            var expenseCategory = await expenseCategoryRepository.Get(request.Id.Value);
            expenseCategory.Name = request.Name;
            expenseCategoryRepository.Update(expenseCategory);
        }
        else
        {
            var expenseCategory = new ExpenseCategory(request.Name);
            expenseCategoryRepository.Add(expenseCategory);
        }

        await unitOfWork.Commit();
    }
}