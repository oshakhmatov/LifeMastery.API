using LifeMastery.Core;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Jobs.Models;
using LifeMastery.Core.Modules.WeightControl.Models;
using LifeMastery.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LifeMastery.Infrastructure.Data;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    private readonly IOptionsSnapshot<DbOptions> optionsSnapshot;

    public AppDbContext(DbContextOptions options, IOptionsSnapshot<DbOptions> optionsSnapshot) : base(options)
    {
        this.optionsSnapshot = optionsSnapshot;
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<WeightRecord> WeightRecords { get; set; }
    public DbSet<HealthInfo> HealthInfos { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<RegularPayment> RegularPayments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<EmailSubscription> EmailSubscriptions { get; set; }
    public DbSet<ExpenseCreationRule> ExpenseCreationRules { get; set; }
    public DbSet<FinanceInfo> FinanceInfo { get; set; }

    public async Task Commit()
    {
        await SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(optionsSnapshot.Value.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}