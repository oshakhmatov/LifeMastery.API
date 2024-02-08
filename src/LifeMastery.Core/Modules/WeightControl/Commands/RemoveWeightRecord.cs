using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class RemoveWeightRecord : CommandBase<string>
{
    private readonly IWeightRecordRepository weightRecordRepository;

    public RemoveWeightRecord(IUnitOfWork unitOfWork, IWeightRecordRepository weightRecordRepository) : base(unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
    }

    protected override async Task OnExecute(string date, CancellationToken token)
    {
        var dateOnly = DateOnly.Parse(date);

        var weightRecord = await weightRecordRepository.Get(dateOnly)
            ?? throw new Exception($"WeightRecord with Date={dateOnly} was not found");

        weightRecordRepository.Remove(weightRecord);
    }
}