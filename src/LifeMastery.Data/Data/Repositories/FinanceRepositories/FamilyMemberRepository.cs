using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public sealed class FamilyMemberRepository(AppDbContext dbContext) : RepositoryBase<FamilyMember>(dbContext), IFamilyMemberRepository
{
    public async Task<FamilyMember?> Get(int id, CancellationToken token = default)
    {
        return await dbContext.FamilyMembers
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(token);
    }

    public async Task<FamilyMember[]> List(CancellationToken token = default)
    {
        return await dbContext.FamilyMembers
            .ToArrayAsync(token);
    }
}