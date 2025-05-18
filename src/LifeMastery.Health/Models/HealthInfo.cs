using LifeMastery.Health.Enums;

namespace LifeMastery.Health.Models;

public class HealthInfo
{
    public int UserId { get; set; }
    public int Height { get; set; }
    public Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
}
