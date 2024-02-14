using LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public class UpdateExpenseCommand
{
    public required int ExpenseId { get; init; }
    public required decimal Amount { get; init; }
    public required DateOnly Date { get; init; }
    public required string? Note { get; init; }
    public required int? CategoryId { get; init; }
}

public class UpdateExpense : IUpdateExpense
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public UpdateExpense(IExpenseRepository expenseRepository, IExpenseCategoryRepository expenseCategoryRepository)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task Execute(UpdateExpenseCommand command, CancellationToken token = default)
    {
        var expenseToUpdate = await expenseRepository.Get(command.ExpenseId, token)
            ?? throw new ApplicationException($"Expense with ID '{command.ExpenseId}' was not found.");

        expenseToUpdate.Amount = command.Amount;
        expenseToUpdate.Note = command.Note;
        expenseToUpdate.Date = command.Date;
        expenseToUpdate.Category = await GetCategory(command.CategoryId, token);
    }

    private async Task<ExpenseCategory?> GetCategory(int? categoryId, CancellationToken token = default)
    {
        if (categoryId is null)
            return null;

        return await expenseCategoryRepository.Get(categoryId.Value, token)
            ?? throw new ApplicationException($"Expense category with ID '{categoryId.Value}' was not found.");
    }
}
