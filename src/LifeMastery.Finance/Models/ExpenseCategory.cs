namespace LifeMastery.Finance.Models;

public class ExpenseCategory
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public bool IsFood { get; set; }
    public string? Color { get; set; }
    public virtual ICollection<Expense> Expenses { get; set; } = [];
    public virtual FamilyMember? FamilyMember { get; set; }

    public ExpenseCategory(string name, bool isFood)
    {
        Name = name;
        IsFood = isFood;
    }

    protected ExpenseCategory()
    {
    }
}
