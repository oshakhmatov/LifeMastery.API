using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.FamilyMembers;

public sealed class RemoveFamilyMember(
    IUnitOfWork unitOfWork,
    IFamilyMemberRepository familyMemberRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var expense = await familyMemberRepository.Get(id, token)
            ?? throw new Exception($"FamilyMember with ID={id} was not found.");

        familyMemberRepository.Remove(expense);
    }
}