using LifeMastery.Domain.Abstractions;
using LifeMastery.Agenda.DataTransferObjects;
using LifeMastery.Agenda.Enums;
using LifeMastery.Agenda.Repositories;

namespace LifeMastery.Agenda.Commands;

public sealed class GetJobs(IJobRepository jobRepository) : ICommand<Unit, JobsViewModel>
{
    public async Task<JobsViewModel> Execute(Unit _, CancellationToken token)
    {
        var toDoJobs = await jobRepository.GetAllNotCompletedAsync(token);

        return new JobsViewModel
        {
            BacklogJobs = toDoJobs.Where(j => j.Group == JobGroup.Backlog).ToArray(),
            WeekJobs = toDoJobs.Where(j => j.Group == JobGroup.Week).ToArray(),
            DayJobs = toDoJobs.Where(j => j.Group == JobGroup.Day).ToArray(),
        };
    }
}
