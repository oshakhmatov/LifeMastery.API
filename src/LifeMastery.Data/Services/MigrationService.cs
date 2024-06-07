using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Services;

public class MigrationService(AppDbContext dbContext) : IMigrationService, IDisposable
{
    public async Task Migrate()
    {
        await dbContext.Database.MigrateAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
