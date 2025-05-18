using LifeMastery.Finance.Models;
using LifeMastery.Finance.Repositories;

namespace LifeMastery.Finance.Services;

public class EarningsResolver(IEarningRepository earnings)
{
    public async Task<Earning[]> Resolve(Earning[] current, FamilyMember[] members, int year, int month, CancellationToken token)
    {
        var result = new List<Earning>(current);

        foreach (var member in members)
        {
            if (current.Any(e => e.FamilyMember == member))
                continue;

            var latest = await earnings.GetLatestByFamilyMember(member, token);
            var newEarning = new Earning(
                latest?.Amount ?? 0,
                year,
                month,
                member);

            earnings.Add(newEarning);
            result.Add(newEarning);
        }

        return result.ToArray();
    }
}
