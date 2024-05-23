using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.WeightControl.Repositories;
using System.Globalization;

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
        var dateOnly = DateOnly.Parse(date, new CultureInfo("ru-RU"));

        var weightRecord = await weightRecordRepository.Get(dateOnly)
            ?? throw new Exception($"WeightRecord with Date={dateOnly} was not found");

        weightRecordRepository.Remove(weightRecord);
    }
}