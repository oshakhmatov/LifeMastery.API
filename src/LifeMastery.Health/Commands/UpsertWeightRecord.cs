using LifeMastery.Domain.Abstractions;
using LifeMastery.Health.Models;
using LifeMastery.Health.Repositories;

namespace LifeMastery.Health.Commands;

public sealed class UpsertWeightRecord(
    IUnitOfWork unitOfWork,
    IWeightRecordRepository weightRecords) : ICommand<UpsertWeightRecord.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var date = request.Date is not null
            ? DateOnly.Parse(request.Date)
            : DateOnly.FromDateTime(DateTime.Today);

        var weightRecord = await weightRecords.FirstOrDefaultAsync(wr => wr.Date == date, token);

        if (weightRecord is null)
        {
            weightRecord = new WeightRecord(request.Weight);
            weightRecords.Add(weightRecord);
        }
        else
        {
            weightRecord.SetWeight(request.Weight);
        }

        await unitOfWork.Commit(token);
    }

    public record Request(string? Date, double Weight);
}
