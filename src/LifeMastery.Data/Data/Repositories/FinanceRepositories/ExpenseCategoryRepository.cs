using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class ExpenseCategoryRepository : RepositoryBase<ExpenseCategory>, IExpenseCategoryRepository
{
    public ExpenseCategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ExpenseCategory?> Get(int id)
    {
        return await dbContext.ExpenseCategories
            .Include(ec => ec.Expenses)
            .Where(ec => ec.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<ExpenseCategory[]> List()
    {
        return await dbContext.ExpenseCategories
            .OrderBy(ec => ec.Name)
            .ToArrayAsync();
    }

    public async Task<ExpenseCategory?> GetByName(string name)
    {
        return await dbContext.ExpenseCategories
            .Where(ec => ec.Name == name)
            .FirstOrDefaultAsync();
    }
}
