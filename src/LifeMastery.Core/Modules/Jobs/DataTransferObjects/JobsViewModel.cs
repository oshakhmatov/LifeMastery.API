using LifeMastery.Core.Modules.Jobs.Models;

namespace LifeMastery.Core.Modules.Jobs.DataTransferObjects;

public sealed class JobsViewModel
{
    public required Job[] BacklogJobs { get; init; }
    public required Job[] WeekJobs { get; init; }
    public required Job[] DayJobs { get; init; }
}