using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IExpenseRepository
{
    public void Add(Expense expense);
    public void Update(Expense expense);
    public void Remove(Expense expense);
    public Task<Expense?> Get(int id);
    public Task<Expense[]> List();
}
