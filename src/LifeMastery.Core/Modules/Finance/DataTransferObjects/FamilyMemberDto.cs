using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class FamilyMemberDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}

public static class FamilyMemberProjections
{
    public static FamilyMemberDto ToDto(this FamilyMember model)
    {
        return new FamilyMemberDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
