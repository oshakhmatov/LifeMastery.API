using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.FamilyMembers;

public class PutFamilyMemberRequest
{
    public int? Id { get; init; }
    public required string Name { get; init; }
}

public class PutFamilyMember(
    IFamilyMemberRepository familyMemberRepository,
    IUnitOfWork unitOfWork) : CommandBase<PutFamilyMemberRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutFamilyMemberRequest command, CancellationToken token = default)
    {
        if (command.Id is null)
        {
            familyMemberRepository.Put(new FamilyMember(command.Name));
        }
        else
        {
            var currency = await familyMemberRepository.Get(command.Id.Value, token)
                ?? throw new ApplicationException($"FamilyMember with ID '{command.Id}' was not found.");

            currency.Name = command.Name;
        }
    }
}