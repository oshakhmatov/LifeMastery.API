using LifeMastery.Health.Models;

namespace LifeMastery.Health.DataTransferObjects;

public class HealthInfoDto
{
    public int Height { get; set; }
}

public static class HealthInfoProjection
{
    public static HealthInfoDto ToDto(this HealthInfo healthInfo)
    {
        return new HealthInfoDto
        {
            Height = healthInfo.Height
        };
    }
}
