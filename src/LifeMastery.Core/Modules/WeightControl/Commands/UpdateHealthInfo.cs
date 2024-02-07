﻿using LifeMastery.Core.Common;
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

public sealed class UpdateHealthInfo : CommandBase<UpdateHealthInfoRequest>
{
    private readonly IHealthInfoRepository healthInfoRepository;

    public UpdateHealthInfo(IUnitOfWork unitOfWork, IHealthInfoRepository healthInfoRepository) : base(unitOfWork)
    {
        this.healthInfoRepository = healthInfoRepository;
    }

    protected override async Task OnExecute(UpdateHealthInfoRequest request, CancellationToken token)
    {
        var healthInfo = await healthInfoRepository.Get();

        if (healthInfo is null)
        {
            healthInfoRepository.Put(new HealthInfo
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

            healthInfoRepository.Put(healthInfo);
        }
    }
}
