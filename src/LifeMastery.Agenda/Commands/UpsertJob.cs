using LifeMastery.Domain.Abstractions;
using LifeMastery.Agenda.Enums;
using LifeMastery.Agenda.Models;
using LifeMastery.Agenda.Repositories;

namespace LifeMastery.Agenda.Commands;

public sealed class UpsertJob(IJobRepository jobRepository, IUnitOfWork unitOfWork) : ICommand<UpsertJob.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        if (request.Id is not null)
        {
            var job = await jobRepository.GetByIdAsync(request.Id.Value)
                ?? throw new AppException($"Job with ID={request.Id} was not found");

            job.Name = request.Name;
            job.IsCompleted = request.IsCompleted;
            job.Priority = request.Priority;
            job.EstimationMinutes = request.EstimationMinutes;
            job.Deadline = request.Deadline;
            job.Group = request.Group;
        }
        else
        {
            jobRepository.Add(new Job
            {
                Name = request.Name,
                IsCompleted = request.IsCompleted,
                Priority = request.Priority,
                EstimationMinutes = request.EstimationMinutes,
                Deadline = request.Deadline,
                Group = request.Group
            });
        }

        await unitOfWork.Commit(token);
    }

    public record Request(
        int? Id,
        string Name,
        JobPriority? Priority,
        int? EstimationMinutes,
        DateTime? Deadline,
        bool IsCompleted,
        JobGroup Group
    );
}
