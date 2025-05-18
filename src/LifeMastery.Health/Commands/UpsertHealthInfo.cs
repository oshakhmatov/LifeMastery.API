using LifeMastery.Domain.Abstractions;
using LifeMastery.Health.Enums;
using LifeMastery.Health.Models;

namespace LifeMastery.Health.Commands;

public sealed class UpsertHealthInfo(
    IUnitOfWork unitOfWork,
    IRepository<HealthInfo> healthInfo) : ICommand<UpsertHealthInfo.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var healthInfoItem = await healthInfo.FirstOrDefaultAsync(_ => true, token);

        if (healthInfoItem is null)
        {
            healthInfo.Add(new HealthInfo
            {
                UserId = 1,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                Height = request.Height
            });
        }
        else
        {
            healthInfoItem.BirthDate = request.BirthDate;
            healthInfoItem.Gender = request.Gender;
            healthInfoItem.Height = request.Height;
        }

        await unitOfWork.Commit(token);
    }

    public record Request(int Height, Gender Gender, DateOnly BirthDate);
}
