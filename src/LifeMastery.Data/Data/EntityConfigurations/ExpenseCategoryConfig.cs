using LifeMastery.Core.Modules.Finance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeMastery.Infrastructure.Data.EntityConfigurations;

public sealed class ExpenseCategoryConfig : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
