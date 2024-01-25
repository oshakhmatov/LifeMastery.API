namespace LifeMastery.Core.Modules.Finance.Models;

public class ExpenseCreationRule
{
    public int Id { get; set; }
    public string Place {  get; set; }
    public ExpenseCategory Category { get; set; }

    protected ExpenseCreationRule() { }

    public ExpenseCreationRule(string place, ExpenseCategory category)
    {
        Place = place;
        Category = category;
    }
}
