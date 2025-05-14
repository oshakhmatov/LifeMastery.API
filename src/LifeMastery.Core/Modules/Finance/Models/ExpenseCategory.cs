namespace LifeMastery.Core.Modules.Finance.Models;

public class ExpenseCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsFood { get; set; }
    public string? Color { get; set; }
    public List<Expense>? Expenses { get; }
    public FamilyMember? FamilyMember { get; set; }

    public ExpenseCategory(string name, bool isFood)
    {
        Name = name;
        IsFood = isFood;
    }

    protected ExpenseCategory()
    {
    }
}
