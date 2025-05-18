using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Repositories;

public interface IExpenseRepository : IRepository<Expense>
{
    Task<Expense[]> GetByPeriodAsync(int year, int month, CancellationToken token = default);
    Task<ExpenseMonthDto[]> GetAvailableExpenseMonthsAsync(CancellationToken token = default);
}
