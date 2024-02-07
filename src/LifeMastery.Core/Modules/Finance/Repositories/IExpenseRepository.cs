using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IExpenseRepository
{
    public void Put(Expense expense);
    public void Remove(Expense expense);
    public Task<Expense?> Get(int id);
    public Task<Expense[]> List(int year, int month);
    Task<Expense?> GetBySource(string source);
    Task<ExpenseMonthDto[]> GetExpenseMonths();
}
