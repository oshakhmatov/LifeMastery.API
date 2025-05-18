namespace LifeMastery.Finance.Models;

public class EmailSubscription
{
    public int Id { get; private set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<ExpenseCreationRule> Rules { get; set; } = [];
    public virtual ICollection<Expense> Expenses { get; set; } = [];

    public EmailSubscription(string email, bool isActive)
    {
        Email = email;
        IsActive = isActive;
    }

    public ExpenseCreationRule GetRule(int expenseCreationRuleId)
    {
        return Rules.FirstOrDefault(r => r.Id == expenseCreationRuleId) 
            ?? throw new Exception($"Expense creation rule with ID='{expenseCreationRuleId}' was not found.");
    }

    public void AddRule(ExpenseCreationRule rule)
    {
        Rules.Add(rule);
    }

    public void RemoveRuleById(int id)
    {
        var rule = Rules.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception($"Expense creation rule with ID='{id}' was not found.");

        Rules.Remove(rule);
    }

    protected EmailSubscription() { }
}
