using LifeMastery.Domain.Abstractions;
using LifeMastery.Agenda.Repositories;

namespace LifeMastery.Agenda.Commands;

public sealed class RemoveJob(IJobRepository jobRepository, IUnitOfWork unitOfWork) : ICommand<RemoveJob.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var job = await jobRepository.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Job with ID={request.Id} was not found");

        jobRepository.Remove(job);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
