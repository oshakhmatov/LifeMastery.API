using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IRegularPaymentRepository
{
    public Task<RegularPayment[]> List();
    public Task<RegularPayment?> Get(int id);
    public void Add(RegularPayment expense);
    public void Update(RegularPayment expense);
    public void Remove(RegularPayment expense);
}
