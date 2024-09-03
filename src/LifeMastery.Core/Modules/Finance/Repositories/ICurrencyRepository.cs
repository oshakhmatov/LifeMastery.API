using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface ICurrencyRepository
{
    public Task<Currency?> Get(int id, CancellationToken token = default);
    public void Put(Currency currency);
    public void Remove(Currency currency);
    public Task<Currency[]> List(CancellationToken token = default);
    public Task<Currency?> GetByName(string name, CancellationToken token = default);
}