using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class AddWeightRecordRequest
{
    public double Weight { get; set; }
}

public sealed class AddWeightRecord : CommandBase<AddWeightRecordRequest>
{
    private readonly IWeightRecordRepository weightRecordRepository;

    public AddWeightRecord(IUnitOfWork unitOfWork, IWeightRecordRepository weightRecordRepository) : base(unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
    }

    protected override async Task OnExecute(AddWeightRecordRequest request, CancellationToken token)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var weight = Math.Round(request.Weight, 1);

        var existingRecord = await weightRecordRepository.GetLast();
        if (existingRecord is null || existingRecord.Date != today)
        {
            weightRecordRepository.Put(new WeightRecord
            {
                Date = today,
                Weight = weight
            });
        }
        else
        {
            existingRecord.Weight = weight;
            weightRecordRepository.Put(existingRecord);
        }
    }
}
