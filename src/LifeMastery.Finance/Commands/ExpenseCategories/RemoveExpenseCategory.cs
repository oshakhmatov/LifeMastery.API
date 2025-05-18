using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.ExpenseCategories;

public sealed class RemoveExpenseCategory(
    IRepository<ExpenseCategory> categories,
    IUnitOfWork unitOfWork) : ICommand<RemoveExpenseCategory.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var category = await categories.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Expense category with ID '{request.Id}' was not found.");

        if (category.Expenses.Count > 0)
            throw new AppException($"Expense category with ID '{request.Id}' cannot be removed because it contains expenses.");

        categories.Remove(category);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
