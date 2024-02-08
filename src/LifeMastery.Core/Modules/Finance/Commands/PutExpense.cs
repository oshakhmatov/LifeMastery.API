using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutExpenseRequest
{
    public int? Id { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
}

public sealed class PutExpense : CommandBase<PutExpenseRequest>
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public PutExpense(
        IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository,
        IExpenseCategoryRepository expenseCategoryRepository) : base(unitOfWork)
    {
        this.expenseRepository = expenseRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    protected override async Task OnExecute(PutExpenseRequest request, CancellationToken token)
    {
        if (request.Id == null)
        {
            await CreateNewExpense(request);
        }
        else
        {
            await UpdateExistingExpense(request);
        }
    }

    private async Task UpdateExistingExpense(PutExpenseRequest request)
    {
        var expense = await expenseRepository.Get(request.Id!.Value)
            ?? throw new Exception($"Expense with ID '{request.Id!.Value}' was not found.");

        UpdateCommonProperties(expense, request);

        expense.Category = await GetCategory(request.CategoryId);
    }

    private async Task CreateNewExpense(PutExpenseRequest request)
    {
        var expense = new Expense(request.Amount);

        UpdateCommonProperties(expense, request);

        expense.Category = await GetCategory(request.CategoryId);

        expenseRepository.Put(expense);
    }

    private static void UpdateCommonProperties(Expense expense, PutExpenseRequest request)
    {
        expense.Note = request.Note;
        expense.Date = DateOnly.FromDateTime(request.Date);
    }

    private async Task<ExpenseCategory?> GetCategory(int? categoryId)
    {
        if (categoryId is null)
            return null;

        return await expenseCategoryRepository.Get(categoryId.Value)
            ?? throw new Exception($"Expense category with ID '{categoryId.Value}' was not found.");
    }
}