using LifeMastery.Core.Modules.Jobs.Enums;
using LifeMastery.Core.Modules.Jobs.Models;
using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Commands;

public class UpdateJobRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public JobPriority? Priority { get; set; }
    public int? EstimationMinutes { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public JobGroup Group { get; set; }
}

public sealed class UpdateJob
{
    private readonly IJobRepository jobRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateJob(IJobRepository jobRepository, IUnitOfWork unitOfWork)
    {
        this.jobRepository = jobRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Update(UpdateJobRequest request)
    {
        Job job;
        if (request.Id.HasValue)
        {
            job = await jobRepository.GetById(request.Id.Value) ?? throw new Exception($"Job with ID={request.Id} was not found");

            job.Name = request.Name;
            job.IsCompleted = request.IsCompleted;
            job.Priority = request.Priority;
            job.EstimationMinutes = request.EstimationMinutes;
            job.Deadline = request.Deadline;
            job.Group = request.Group;
        }
        else
        {
            job = new Job
            {
                Name = request.Name,
                IsCompleted = request.IsCompleted,
                Priority = request.Priority,
                EstimationMinutes = request.EstimationMinutes,
                Deadline = request.Deadline,
                Group = request.Group
            };
        }

        jobRepository.Put(job);

        await unitOfWork.Commit();
    }
}
