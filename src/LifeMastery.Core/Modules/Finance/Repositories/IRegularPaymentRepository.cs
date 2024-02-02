using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IRegularPaymentRepository
{
    public Task<RegularPayment[]> List(CancellationToken token);
    public Task<RegularPayment?> Get(int id, CancellationToken token);
    public void Add(RegularPayment expense);
    public void Update(RegularPayment expense);
    public void Remove(RegularPayment expense);
}
