﻿using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.ExpenseCategories;

public sealed class PutExpenseCategoryRequest
{
    public int? Id { get; set; }
    public required string Name { get; init; }
    public required bool IsFood { get; init; }
    public string? Color { get; set; }
}

public sealed class PutExpenseCategory(
    IUnitOfWork unitOfWork,
    IExpenseCategoryRepository expenseCategoryRepository) : CommandBase<PutExpenseCategoryRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutExpenseCategoryRequest request, CancellationToken token)
    {
        if (request.Id.HasValue)
        {
            var expenseCategory = await expenseCategoryRepository.Get(request.Id.Value, token)
                ?? throw new Exception($"Expense category with ID '{request.Id}' was not found.");

            expenseCategory.Name = request.Name;
            expenseCategory.IsFood = request.IsFood;
            expenseCategory.Color = request.Color;
        }
        else
        {
            var existingCategory = await expenseCategoryRepository.GetByName(request.Name, token);
            if (existingCategory is not null)
                throw new Exception($"Expense category with name '{request.Name}' already exists.");

            expenseCategoryRepository.Put(new ExpenseCategory(request.Name, request.IsFood)
            {
                Color = request.Color
            });
        }
    }
}