using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class AddWeightRecordRequest
{
    public double Weight { get; set; }
}

public sealed class AddWeightRecord
{
    private readonly IWeightRecordRepository weightRecordRepository;
    private readonly IUnitOfWork unitOfWork;

    public AddWeightRecord(IWeightRecordRepository weightRecordRepository, IUnitOfWork unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(AddWeightRecordRequest request)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var weight = Math.Round(request.Weight, 1);

        var existingRecord = await weightRecordRepository.GetLast();
        if (existingRecord is null || existingRecord.Date != today)
        {
            weightRecordRepository.Add(new WeightRecord
            {
                Date = today,
                Weight = weight
            });
        }
        else
        {
            existingRecord.Weight = weight;
            weightRecordRepository.Update(existingRecord);
        }

        await unitOfWork.Commit();
    }
}
