using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

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
