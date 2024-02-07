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
        if (request.Id.HasValue)
        {
            var expense = await expenseRepository.Get(request.Id.Value);

            expense.Note = request.Note;
            expense.Amount = request.Amount;
            expense.Date = DateOnly.FromDateTime(request.Date);

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
            var expense = new Expense(request.Amount)
            {
                Note = request.Note,
                Date = DateOnly.FromDateTime(request.Date)
            };

            if (request.CategoryId is not null)
            {
                expense.Category = await expenseCategoryRepository.Get(request.CategoryId.Value);
            }

            expenseRepository.Add(expense);
        }
    }
}