using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class CurrencyRepository(AppDbContext dbContext) : RepositoryBase<Currency>(dbContext), ICurrencyRepository
{
    public async Task<Currency?> Get(int id, CancellationToken token = default)
    {
        return await dbContext.Currencies
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<Currency[]> List(CancellationToken token = default)
    {
        return await dbContext.Currencies
            .ToArrayAsync(token);
    }

    public async Task<Currency?> GetByName(string name, CancellationToken token = default)
    {
        return await dbContext.Currencies
            .Where(e => e.Name == name)
            .FirstOrDefaultAsync(token);
    }
}
