using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Repositories;

public interface IEarningRepository : IRepository<Earning>
{
    Task<Earning[]> GetByPeriodAsync(int year, int month, CancellationToken token = default);
    Task<Earning?> GetLatestByFamilyMember(FamilyMember familyMember, CancellationToken cancellationToken);
}
