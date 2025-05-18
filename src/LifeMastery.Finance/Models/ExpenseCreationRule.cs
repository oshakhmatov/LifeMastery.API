namespace LifeMastery.Finance.Models;

public class ExpenseCreationRule
{
    public int Id { get; private set; }
    public string Place { get; set; }
    public virtual ExpenseCategory Category { get; set; }

    public ExpenseCreationRule(string place, ExpenseCategory category)
    {
        Place = place;
        Category = category;
    }

    protected ExpenseCreationRule()
    {
    }
}
