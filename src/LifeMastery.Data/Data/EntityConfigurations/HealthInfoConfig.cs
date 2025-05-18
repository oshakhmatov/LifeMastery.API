using LifeMastery.Health.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeMastery.Infrastructure.Data.EntityConfigurations;

public class HealthInfoConfig : IEntityTypeConfiguration<HealthInfo>
{
    public void Configure(EntityTypeBuilder<HealthInfo> builder)
    {
        builder.HasKey(x => x.UserId);
    }
}
