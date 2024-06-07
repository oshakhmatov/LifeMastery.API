using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseCategoryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required bool IsFood { get; init; }
    public required string? Color { get; init; }

    public static ExpenseCategoryDto FromModel(ExpenseCategory expenseCategory)
    {
        return new ExpenseCategoryDto
        {
            Id = expenseCategory.Id,
            Name = expenseCategory.Name,
            IsFood = expenseCategory.IsFood,
            Color = expenseCategory.Color
        };
    }
}