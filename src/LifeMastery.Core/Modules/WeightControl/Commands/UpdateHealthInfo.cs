using LifeMastery.Core.Modules.WeightControl.Enums;
using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public class UpdateHealthInfoRequest
{
    public int Height { get; set; }
    public Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
}

public sealed class UpdateHealthInfo
{
    private readonly IHealthInfoRepository healthInfoRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateHealthInfo(IHealthInfoRepository healthInfoRepository, IUnitOfWork unitOfWork)
    {
        this.healthInfoRepository = healthInfoRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateHealthInfoRequest request)
    {
        var healthInfo = await healthInfoRepository.Get();

        if (healthInfo is null)
        {
            healthInfoRepository.Add(new HealthInfo
            {
                UserId = 1,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                Height = request.Height
            });
        }
        else
        {
            healthInfo.BirthDate = request.BirthDate;
            healthInfo.Gender = request.Gender;
            healthInfo.Height = request.Height;

            healthInfoRepository.Update(healthInfo);
        }

        await unitOfWork.Commit();
    }
}
