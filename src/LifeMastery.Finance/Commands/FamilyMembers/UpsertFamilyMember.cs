using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.FamilyMembers;

public sealed class UpsertFamilyMember(
    IRepository<FamilyMember> members,
    IUnitOfWork unitOfWork) : ICommand<UpsertFamilyMember.Request>
{
    public async Task Execute(Request command, CancellationToken token)
    {
        if (command.Id is null)
        {
            members.Add(new FamilyMember(command.Name));
        }
        else
        {
            var member = await members.GetByIdAsync(command.Id.Value, token)
                ?? throw new AppException($"FamilyMember with ID '{command.Id}' was not found.");

            member.Name = command.Name;
        }

        await unitOfWork.Commit(token);
    }

    public record Request(int? Id, string Name);
}
