using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Expenses;

public sealed class RemoveExpense(IRepository<Expense> expenses, IUnitOfWork unitOfWork) : ICommand<RemoveExpense.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var expense = await expenses.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Expense with ID '{request.Id}' was not found.");

        expenses.Remove(expense);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
