using LifeMastery.Core.Modules.Jobs.DataTransferObjects;
using LifeMastery.Core.Modules.Jobs.Enums;
using LifeMastery.Core.Modules.Jobs.Repositories;

namespace LifeMastery.Core.Modules.Jobs.Queries;

public sealed class GetJobs
{
    private readonly IJobRepository jobRepository;

    public GetJobs(IJobRepository jobRepository)
    {
        this.jobRepository = jobRepository;
    }

    public async Task<JobsViewModel> Execute()
    {
        var toDoJobs = await jobRepository.Get(completed: false);

        return new JobsViewModel
        {
            BacklogJobs = toDoJobs.Where(j => j.Group == JobGroup.Backlog).ToArray(),
            WeekJobs = toDoJobs.Where(j => j.Group == JobGroup.Week).ToArray(),
            DayJobs = toDoJobs.Where(j => j.Group == JobGroup.Day).ToArray(),
        };
    }
}
