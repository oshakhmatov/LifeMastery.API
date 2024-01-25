using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Services.Abstractions
{
    public interface IExpenseCategoryResolver
    {
        Task<ExpenseCategory?> ResolveFromPlace(string place);
    }
}