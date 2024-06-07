namespace LifeMastery.Core.Modules.Finance.Models;

public class ExpenseCreationRule
{
    public int Id { get; set; }
    public string Place { get; set; }
    public ExpenseCategory Category { get; set; }

    public ExpenseCreationRule(string place, ExpenseCategory category)
    {
        Place = place;
        Category = category;
    }

    protected ExpenseCreationRule()
    {
    }
}
