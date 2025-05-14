using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IFamilyMemberRepository
{
    public Task<FamilyMember?> Get(int id, CancellationToken token = default);
    public Task<FamilyMember[]> List(CancellationToken token = default);
    public void Put(FamilyMember familyMember);
    public void Remove(FamilyMember familyMember);
}
