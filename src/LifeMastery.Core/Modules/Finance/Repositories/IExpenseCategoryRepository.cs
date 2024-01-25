using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IExpenseCategoryRepository
{
    public Task<ExpenseCategory?> Get(int id);
    public Task<ExpenseCategory?> GetByName(string name);
    public void Add(ExpenseCategory expenseCategory);
    public void Update(ExpenseCategory expenseCategory);
    public void Remove(ExpenseCategory expenseCategory);
    public Task<ExpenseCategory[]> List();
}
