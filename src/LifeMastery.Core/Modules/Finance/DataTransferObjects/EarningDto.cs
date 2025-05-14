using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class EarningDto
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required string FamilyMemberName { get; init; }
    public required int PeriodYear { get; init; }
    public required int PeriodMonth { get; init; }
}

public static class EarningProjections
{
    public static EarningDto ToDto(this Earning model)
    {
        return new EarningDto
        {
            Id = model.Id,
            Amount = model.Amount,
            FamilyMemberName = model.FamilyMember.Name,
            PeriodMonth = model.PeriodMonth,
            PeriodYear = model.PeriodYear
        };
    }
}
