using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class DeleteWeightRecord
{
    private readonly IWeightRecordRepository weightRecordRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteWeightRecord(IWeightRecordRepository weightRecordRepository, IUnitOfWork unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(DateOnly date)
    {
        var weightRecord = await weightRecordRepository.Get(date);
        if (weightRecord is null)
            throw new Exception($"WeightRecord with Date={date} was not found");

        weightRecordRepository.Remove(weightRecord);

        await unitOfWork.Commit();
    }
}