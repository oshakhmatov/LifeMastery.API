using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static ExpenseCategoryDto FromModel(ExpenseCategory expenseCategory)
    {
        return new ExpenseCategoryDto
        {
            Id = expenseCategory.Id,
            Name = expenseCategory.Name
        };
    }
}