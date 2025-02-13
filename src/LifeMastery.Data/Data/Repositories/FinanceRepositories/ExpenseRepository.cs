using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Expense?> Get(int id, CancellationToken token = default)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<Expense?> GetByTransactionId(string source, CancellationToken token = default)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Source == source)
            .FirstOrDefaultAsync(token);
    }

    public async Task<Expense[]> List(int year, int month, CancellationToken token = default)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date.Year == year && e.Date.Month == month)
            .OrderByDescending(e => e.Date)
            .ThenByDescending(e => e.Id)
            .ToArrayAsync(token);
    }

    public async Task<ExpenseMonthDto[]> GetExpenseMonths(CancellationToken token = default)
    {
        return await dbContext.Expenses
            .Select(e => new ExpenseMonthDto
            {
                Month = e.Date.Month,
                Year = e.Date.Year
            })
            .Distinct()
            .OrderByDescending(e => e.Year)
            .ThenBy(e => e.Month)
            .ToArrayAsync(token);
    } 
}
