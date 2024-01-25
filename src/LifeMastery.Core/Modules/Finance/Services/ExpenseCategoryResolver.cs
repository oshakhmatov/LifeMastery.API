using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Services;

public class ExpenseCategoryResolver : IExpenseCategoryResolver
{
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public ExpenseCategoryResolver(IExpenseCategoryRepository expenseCategoryRepository)
    {
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    public async Task<ExpenseCategory?> ResolveFromPlace(string place)
    {
        var productsCategory = await expenseCategoryRepository.GetByName("Продукты");
        if (productsCategory == null)
            return null;

        if (place == "Mikromarket 110 Novi Sad RS")
        {
            return productsCategory;
        }

        return null;
    }
}
