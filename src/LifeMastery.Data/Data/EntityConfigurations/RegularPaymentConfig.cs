using LifeMastery.Core.Modules.Finance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeMastery.Infrastructure.Data.EntityConfigurations;

public sealed class RegularPaymentConfig : IEntityTypeConfiguration<RegularPayment>
{
    public void Configure(EntityTypeBuilder<RegularPayment> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
