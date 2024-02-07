using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveExpense : CommandBase<int>
{
    private readonly IExpenseRepository expenseRepository;

    public RemoveExpense(
        IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository) : base(unitOfWork)
    {
        this.expenseRepository = expenseRepository;
    }

    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expense = await expenseRepository.Get(id)
            ?? throw new Exception($"Expense with ID={id} was not found");

        expenseRepository.Remove(expense);
    }
}
