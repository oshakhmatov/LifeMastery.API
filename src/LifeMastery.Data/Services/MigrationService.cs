using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Services;

public class MigrationService : IMigrationService, IDisposable
{
    private readonly AppDbContext dbContext;

    public MigrationService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Migrate()
    {
        await dbContext.Database.MigrateAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
