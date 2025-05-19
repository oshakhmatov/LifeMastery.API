using LifeMastery.Domain.Abstractions;
using LifeMastery.Health.Repositories;

namespace LifeMastery.Health.Commands;

public sealed class RemoveWeightRecord(
    IUnitOfWork unitOfWork,
    IWeightRecordRepository weightRecords) : ICommand<RemoveWeightRecord.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var date = DateOnly.Parse(request.Date);

        var weightRecord = await weightRecords.FirstOrDefaultAsync(wr => wr.Date == date, token)
            ?? throw new Exception($"WeightRecord with Date={date} was not found");

        weightRecords.Remove(weightRecord);
        await unitOfWork.Commit(token);
    }

    public record Request(string Date);
}
