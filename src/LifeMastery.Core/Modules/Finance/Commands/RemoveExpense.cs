using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveExpense
{
    private readonly IExpenseRepository expenseRepository;
    private readonly IUnitOfWork unitOfWork;

    public RemoveExpense(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
    {
        this.expenseRepository = expenseRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var expense = await expenseRepository.Get(id);
        if (expense is null)
            throw new Exception($"Expense with ID={id} was not found");

        expenseRepository.Remove(expense);

        await unitOfWork.Commit();
    }
}
