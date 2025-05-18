using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class ExpenseCategoryRepository(AppDbContext db) : Repository<ExpenseCategory>(db), IExpenseCategoryRepository
{
    public async Task<ExpenseCategory[]> GetAllOrderedByNameAsync(CancellationToken token)
    {
        return await db.ExpenseCategories
            .OrderBy(ec => ec.Name)
            .Include(ec => ec.FamilyMember)
            .ToArrayAsync(token);
    }
}
