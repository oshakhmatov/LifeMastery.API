using LifeMastery.Common;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class ExpenseRepository(AppDbContext db) : Repository<Expense>(db), IExpenseRepository
{
    public async Task<Expense[]> GetByPeriodAsync(int year, int month, CancellationToken token = default)
    {
        return await db.Expenses
           .Include(e => e.Category)
           .Where(e => e.Date.Year == year && e.Date.Month == month)
           .OrderByDescending(e => e.Date)
           .ThenByDescending(e => e.Id)
           .ToArrayAsync(token);
    }

    public Task<ExpenseMonthDto[]> GetAvailableExpenseMonthsAsync(CancellationToken token = default)
    {
        return db.Expenses
            .Select(e => new { e.Date.Year, e.Date.Month })
            .Distinct()
            .OrderByDescending(e => e.Year)
            .ThenByDescending(e => e.Month)
            .Select(e => new ExpenseMonthDto
            {
                Year = e.Year,
                Month = e.Month,
                Name = $"{DateHelper.GetMonthName(e.Month)} {e.Year}"
            })
            .ToArrayAsync(token);
    }
}
