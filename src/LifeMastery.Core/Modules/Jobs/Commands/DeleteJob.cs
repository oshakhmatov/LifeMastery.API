using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Commands;

public sealed class RemoveJob : CommandBase<int>
{
    private readonly IJobRepository jobRepository;

    public RemoveJob(IUnitOfWork unitOfWork, IJobRepository jobRepository) : base(unitOfWork)
    {
        this.jobRepository = jobRepository;
    }

    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var job = await jobRepository.GetById(id)
            ?? throw new Exception($"Job with ID={id} was not found");

        jobRepository.Remove(job);
    }
}
