using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IEarningRepository
{
    Task<Earning[]> List(int year, int month, CancellationToken token);
    Task<Earning?> Get(int id, CancellationToken token);
    public void Put(Earning earning);
    Task<Earning?> GetLatestByFamilyMember(FamilyMember familyMember, CancellationToken cancellationToken);
}
