using LifeMastery.Agenda.Models;

namespace LifeMastery.Agenda.DataTransferObjects;

public sealed class JobsViewModel
{
    public required Job[] BacklogJobs { get; init; }
    public required Job[] WeekJobs { get; init; }
    public required Job[] DayJobs { get; init; }
}