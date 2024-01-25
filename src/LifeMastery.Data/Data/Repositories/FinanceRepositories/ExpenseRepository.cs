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

    public async Task<Expense[]> List()
    {
        return await dbContext.Expenses
            .Include(e => e.Category)
            .OrderByDescending(e => e.Id)
            .ToArrayAsync();
    }
}
