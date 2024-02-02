using System.Collections.ObjectModel;

namespace LifeMastery.Core.Modules.Finance.Models;

public class EmailSubscription
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }

    private readonly IList<ExpenseCreationRule> rules;
    public IReadOnlyCollection<ExpenseCreationRule> Rules => new ReadOnlyCollection<ExpenseCreationRule>(rules);

    private readonly IList<Expense> expenses;
    public IReadOnlyCollection<Expense> Expenses => expenses.AsReadOnly();

    protected EmailSubscription() { }

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
        rules.Add(rule);
    }

    public void RemoveRuleById(int id)
    {
        var rule = Rules.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception($"Expense creation rule with ID='{id}' was not found.");

        rules.Remove(rule);
    }
}
