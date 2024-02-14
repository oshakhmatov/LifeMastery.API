using LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public class AddExpenseCommand
{
    public required decimal Amount { get; init; }
    public required DateOnly Date { get; init; }
    public required string? Note { get; init; }
    public required int? CategoryId { get; init; }
}

public class AddExpense : IAddExpense
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public AddExpense(IExpenseRepository expenseRepository, IExpenseCategoryRepository expenseCategoryRepository)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task Execute(AddExpenseCommand command, CancellationToken token = default)
    {
        expenseRepository.Put(new Expense(command.Amount)
        {
            Note = command.Note,
            Date = command.Date,
            Category = await GetCategory(command.CategoryId, token)
        });
    }

    private async Task<ExpenseCategory?> GetCategory(int? categoryId, CancellationToken token = default)
    {
        if (categoryId is null)
            return null;

        return await expenseCategoryRepository.Get(categoryId.Value, token)
            ?? throw new ApplicationException($"Expense category with ID '{categoryId.Value}' was not found.");
    }
}
