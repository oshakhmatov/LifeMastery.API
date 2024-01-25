using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class UpdateWeightRecordRequest
{
    public double Weight { get; set; }
}

public sealed class UpdateWeightRecord
{
    private readonly IWeightRecordRepository weightRecordRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateWeightRecord(IWeightRecordRepository weightRecordRepository, IUnitOfWork unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(DateOnly date, UpdateWeightRecordRequest request)
    {
        var weightRecord = await weightRecordRepository.Get(date);
        if (weightRecord is null)
            throw new Exception($"WeightRecord with Date={date} was not found");

        var weight = Math.Round(request.Weight, 1);

        weightRecord.Weight = weight;

        weightRecordRepository.Update(weightRecord);

        await unitOfWork.Commit();
    }
}