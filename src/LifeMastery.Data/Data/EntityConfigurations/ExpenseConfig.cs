using LifeMastery.Core.Modules.Finance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeMastery.Infrastructure.Data.EntityConfigurations;

public sealed class ExpenseConfig : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Date).HasColumnType("date").HasDefaultValueSql("CURRENT_DATE");
        builder.HasOne(e => e.Category).WithMany(ec => ec.Expenses);
        builder.HasOne(e => e.EmailSubscription).WithMany(es => es.Expenses);
    }
}