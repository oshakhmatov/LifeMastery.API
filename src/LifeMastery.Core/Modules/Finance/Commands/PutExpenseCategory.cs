using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutExpenseCategoryRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
}

public sealed class PutExpenseCategory : CommandBase<PutExpenseCategoryRequest>
{
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public PutExpenseCategory(
        IUnitOfWork unitOfWork, 
        IExpenseCategoryRepository expenseCategoryRepository) : base(unitOfWork)
    {
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    protected override async Task OnExecute(PutExpenseCategoryRequest request, CancellationToken token)
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
    }
}