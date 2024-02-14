using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IExpenseRepository
{
    public void Put(Expense expense);
    public void Remove(Expense expense);
    public Task<Expense?> Get(int id, CancellationToken token = default);
    public Task<Expense[]> List(int year, int month, CancellationToken token = default);
    Task<Expense?> GetBySource(string source, CancellationToken token = default);
    Task<ExpenseMonthDto[]> GetExpenseMonths(CancellationToken token = default);
}
