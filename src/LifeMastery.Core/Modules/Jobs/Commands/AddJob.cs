using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Jobs.Enums;
using LifeMastery.Core.Modules.Jobs.Models;
using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Commands;

public sealed class AddJobRequest
{
    public string Name { get; set; }
}

public sealed class AddJob : CommandBase<AddJobRequest>
{
    private readonly IJobRepository jobRepository;

    public AddJob(IUnitOfWork unitOfWork, IJobRepository jobRepository) : base(unitOfWork)
    {
        this.jobRepository = jobRepository;
    }

    protected override Task OnExecute(AddJobRequest request, CancellationToken token)
    {
        jobRepository.Put(new Job
        {
            Name = request.Name,
            Priority = JobPriority.Medium
        });

        return Task.CompletedTask;
    }
}
