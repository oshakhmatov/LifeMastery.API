﻿using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public class PutExpenseRequest
{
    public int? Id { get; init; }
    public required decimal Amount { get; init; }
    public required int CurrencyId { get; init; }
    public DateTime? Date { get; init; }
    public string? Note { get; init; }
    public int? CategoryId { get; init; }
}

public class PutExpense(
    IExpenseRepository expenseRepository,
    IExpenseCategoryRepository expenseCategoryRepository,
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) : CommandBase<PutExpenseRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutExpenseRequest command, CancellationToken token = default)
    {
        var date = DateOnly.FromDateTime(DateTime.Today);
        if (command.Date is not null)
        {
            date = DateOnly.FromDateTime(command.Date.Value);
        }

        if (command.Id is null)
        {
            expenseRepository.Put(new Expense(command.Amount, await GetCurrency(command.CurrencyId, token))
            {
                Note = command.Note,
                Date = date,
                Category = await GetCategory(command.CategoryId, token)
            });
        }
        else
        {
            var expenseToUpdate = await expenseRepository.Get(command.Id.Value, token)
                ?? throw new ApplicationException($"Expense with ID '{command.Id}' was not found.");

            expenseToUpdate.Amount = command.Amount;
            expenseToUpdate.Note = command.Note;
            expenseToUpdate.Date = date;
            expenseToUpdate.Category = await GetCategory(command.CategoryId, token);
            expenseToUpdate.Currency = await GetCurrency(command.CurrencyId, token);
        }
    }

    private async Task<ExpenseCategory?> GetCategory(int? categoryId, CancellationToken token = default)
    {
        if (categoryId is null)
            return null;

        return await expenseCategoryRepository.Get(categoryId.Value, token)
            ?? throw new ApplicationException($"Expense category with ID '{categoryId.Value}' was not found.");
    }

    private async Task<Currency> GetCurrency(int currencyId, CancellationToken token = default)
    {
        return await currencyRepository.Get(currencyId, token)
            ?? throw new ApplicationException($"Currency with ID '{currencyId}' was not found.");
    }
}
