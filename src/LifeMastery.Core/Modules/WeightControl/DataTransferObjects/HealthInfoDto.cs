using LifeMastery.Core.Modules.WeightControl.Enums;
using LifeMastery.Core.Modules.WeightControl.Models;

namespace LifeMastery.Core.Modules.WeightControl.DataTransferObjects;

public class HealthInfoDto
{
    public int Height { get; set; }
    public Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }

    public static HealthInfoDto FromModel(HealthInfo healthInfo)
    {
        return new HealthInfoDto
        {
            BirthDate = healthInfo.BirthDate,
            Gender = healthInfo.Gender,
            Height = healthInfo.Height
        };
    }
}
