using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Expenses;

public sealed class UpsertExpense(
    IRepository<Expense> expenses,
    IRepository<ExpenseCategory> categories,
    IRepository<Currency> currencies,
    IUnitOfWork unitOfWork) : ICommand<UpsertExpense.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var date = DateOnly.FromDateTime(request.Date ?? DateTime.Today);

        if (request.Id is null)
        {
            var expense = new Expense(request.Amount, await GetCurrency(request.CurrencyId, token))
            {
                Note = request.Note,
                Date = date,
                Category = await GetCategory(request.CategoryId, token)
            };

            expenses.Add(expense);
        }
        else
        {
            var existing = await expenses.GetByIdAsync(request.Id.Value, token)
                ?? throw new AppException($"Expense with ID '{request.Id}' was not found.");

            existing.Amount = request.Amount;
            existing.Note = request.Note;
            existing.Date = date;
            existing.Category = await GetCategory(request.CategoryId, token);
            existing.Currency = await GetCurrency(request.CurrencyId, token);
        }

        await unitOfWork.Commit(token);
    }

    private async Task<ExpenseCategory?> GetCategory(int? categoryId, CancellationToken token)
    {
        if (categoryId is null)
            return null;

        return await categories.GetByIdAsync(categoryId.Value, token)
            ?? throw new AppException($"Expense category with ID '{categoryId.Value}' was not found.");
    }

    private async Task<Currency> GetCurrency(int currencyId, CancellationToken token)
    {
        return await currencies.GetByIdAsync(currencyId, token)
            ?? throw new AppException($"Currency with ID '{currencyId}' was not found.");
    }

    public record Request(
        int? Id,
        decimal Amount,
        int CurrencyId,
        DateTime? Date,
        string? Note,
        int? CategoryId);
}
