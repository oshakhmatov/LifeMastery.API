using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Core.Modules.WeightControl.Repositories;

namespace LifeMastery.Core.Modules.WeightControl.Commands;

public sealed class PutWeightRecordRequest
{
    public string? Date { get; set; }
    public double Weight { get; set; }
}

public sealed class PutWeightRecord : CommandBase<PutWeightRecordRequest>
{
    private readonly IWeightRecordRepository weightRecordRepository;

    public PutWeightRecord(
        IUnitOfWork unitOfWork,
        IWeightRecordRepository weightRecordRepository) : base(unitOfWork)
    {
        this.weightRecordRepository = weightRecordRepository;
    }

    protected override async Task OnExecute(PutWeightRecordRequest request, CancellationToken token)
    {
        WeightRecord? weightRecord;
        if (request.Date == null)
        {
            weightRecord = await weightRecordRepository.Get(DateOnly.FromDateTime(DateTime.Today));
            if (weightRecord is null)
            {
                weightRecord = new WeightRecord(request.Weight);
                weightRecordRepository.Put(weightRecord);
            }
            else
            {
                weightRecord.SetWeight(request.Weight);
            }
        }
        else
        {
            var date = DateOnly.Parse(request.Date);

            weightRecord = await weightRecordRepository.Get(date)
                ?? throw new Exception($"WeightRecord with Date={date} was not found");

            weightRecord.SetWeight(request.Weight);
        }
    }
}