using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public sealed class RemoveExpense(
    IUnitOfWork unitOfWork,
    IExpenseRepository expenseRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expense = await expenseRepository.Get(id, token)
            ?? throw new Exception($"Expense with ID={id} was not found");

        expenseRepository.Remove(expense);
    }
}
