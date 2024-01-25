using LifeMastery.Core.Modules.Jobs.Enums;
using LifeMastery.Core.Modules.Jobs.Models;
using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Commands;

public sealed class AddJobRequest
{
    public string Name { get; set; }
}

public sealed class AddJob
{
    private readonly IJobRepository jobRepository;
    private readonly IUnitOfWork unitOfWork;

    public AddJob(IJobRepository jobRepository, IUnitOfWork unitOfWork)
    {
        this.jobRepository = jobRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(AddJobRequest request)
    {
        jobRepository.Add(new Job
        {
            Name = request.Name,
            Priority = JobPriority.Medium
        });

        await unitOfWork.Commit();
    }
}
