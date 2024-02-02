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

    public async Task<Expense?> Get(int id)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Expense?> GetBySource(string source)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Source == source)
            .FirstOrDefaultAsync();
    }

    public async Task<Expense[]> List(int year, int month)
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date.Year == year && e.Date.Month == month)
            .OrderByDescending(e => e.Date)
            .ThenByDescending(e => e.Id)
            .ToArrayAsync();
    }

    public async Task<ExpenseMonthDto[]> GetExpenseMonths()
    {
        return await dbContext.Expenses
            .OrderByDescending(e => e.Date)
            .Select(e => new ExpenseMonthDto
            {
                Month = e.Date.Month,
                Year = e.Date.Year
            })
            .Distinct()
            .ToArrayAsync();
    } 
}
