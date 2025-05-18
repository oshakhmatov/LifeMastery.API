using LifeMastery.Infrastructure.Services.Abstractions;

namespace LifeMastery.ScenariusTests;

public class MockMigrationService : IMigrationService
{
    public void Dispose()
    {
    }

    public Task Migrate()
    {
        return Task.CompletedTask;
    }
}
