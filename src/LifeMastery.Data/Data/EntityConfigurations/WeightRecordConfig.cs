using LifeMastery.Core.Modules.WeightControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeMastery.Infrastructure.Data.EntityConfigurations;

public class WeightRecordConfig : IEntityTypeConfiguration<WeightRecord>
{
    public void Configure(EntityTypeBuilder<WeightRecord> builder)
    {
        builder.HasKey(x => x.Date);
    }
}
