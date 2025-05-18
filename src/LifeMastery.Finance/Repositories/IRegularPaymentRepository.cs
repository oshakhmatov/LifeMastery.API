using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Repositories;

public interface IRegularPaymentRepository : IRepository<RegularPayment>
{
    public Task<RegularPayment[]> GetAllOrderedByNewestAsync(CancellationToken token);
}
