using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Repositories;

public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
{
    Task<ExpenseCategory[]> GetAllOrderedByNameAsync(CancellationToken token);
}
