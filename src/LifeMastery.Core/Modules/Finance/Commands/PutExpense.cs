using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutExpenseRequest
{
    public int? Id { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    public string? Note { get; set; }
    public DateOnly Date { get; set; }
}

public sealed class PutExpense
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;
    private readonly IUnitOfWork unitOfWork;

    public PutExpense(
        IExpenseRepository expenseRepository,
        IUnitOfWork unitOfWork,
        IExpenseCategoryRepository expenseCategoryRepository)
    {
        this.expenseRepository = expenseRepository;
        this.unitOfWork = unitOfWork;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task Execute(PutExpenseRequest request)
    {
        if (request.Id.HasValue)
        {
            var expense = await expenseRepository.Get(request.Id.Value);

            expense.Note = request.Note;
            expense.Amount = request.Amount;
            expense.Date = request.Date;

            if (request.CategoryId is not null)
            {
                expense.Category = await expenseCategoryRepository.Get(request.CategoryId.Value);
            }
            else
            {
                expense.Category = null;
            }

            expenseRepository.Update(expense);
        }
        else
        {
            var expense = new Expense(request.Amount);

            expense.Note = request.Note;

            if (request.CategoryId is not null)
            {
                expense.Category = await expenseCategoryRepository.Get(request.CategoryId.Value);
            }

            expenseRepository.Add(expense);
        }
        

        await unitOfWork.Commit();
    }
}