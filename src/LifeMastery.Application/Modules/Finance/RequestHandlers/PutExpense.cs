using LifeMastery.Core;
using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;

namespace LifeMastery.Application.Modules.Finance.RequestHandlers;

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
    private readonly IAddExpense addExpense;
    private readonly IUpdateExpense updateExpense;

    public PutExpense(IUnitOfWork unitOfWork, IAddExpense addExpense, IUpdateExpense updateExpense) : base(unitOfWork)
    {
        this.addExpense = addExpense;
        this.updateExpense = updateExpense;
    }

    protected override async Task OnExecute(PutExpenseRequest request, CancellationToken token = default)
    {
        if (request.Id == null)
        {
            await addExpense.Execute(new AddExpenseCommand
            {
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Note = request.Note,
                Date = DateOnly.FromDateTime(request.Date)
            }, token);
        }
        else
        {
            await updateExpense.Execute(new UpdateExpenseCommand
            {
                ExpenseId = request.Id.Value,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Note = request.Note,
                Date = DateOnly.FromDateTime(request.Date)
            }, token);
        }
    }
}
