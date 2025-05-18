using LifeMastery.Agenda.Enums;

namespace LifeMastery.Agenda.Models;

public class Job
{
    public int Id { get; set; }
    public string Name { get; set; }
    public JobPriority? Priority { get; set; }
    public int? EstimationMinutes { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public JobGroup Group { get; set; }
}
