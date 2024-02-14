using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IExpenseCategoryRepository
{
    public Task<ExpenseCategory?> Get(int id, CancellationToken token = default);
    public Task<ExpenseCategory?> GetByName(string name, CancellationToken token = default);
    public void Put(ExpenseCategory expenseCategory);
    public void Remove(ExpenseCategory expenseCategory);
    public Task<ExpenseCategory[]> List(CancellationToken token = default);
}
