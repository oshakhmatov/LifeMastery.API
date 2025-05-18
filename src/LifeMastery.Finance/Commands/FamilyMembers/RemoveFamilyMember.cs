using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.FamilyMembers;

public sealed class RemoveFamilyMember(
    IRepository<FamilyMember> members,
    IUnitOfWork unitOfWork) : ICommand<RemoveFamilyMember.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var member = await members.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"FamilyMember with ID={request.Id} was not found.");

        members.Remove(member);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
