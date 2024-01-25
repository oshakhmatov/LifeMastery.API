using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Commands;

public sealed class DeleteJob
{
    private readonly IJobRepository jobRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteJob(IJobRepository jobRepository, IUnitOfWork unitOfWork)
    {
        this.jobRepository = jobRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Delete(int id)
    {
        var job = await jobRepository.GetById(id);
        if (job is null)
            throw new Exception($"Job with ID={id} was not found");

        jobRepository.Remove(job);

        await unitOfWork.Commit();
    }
}
