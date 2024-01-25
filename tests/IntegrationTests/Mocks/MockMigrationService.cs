using LifeMastery.Infrastructure.Services.Abstractions;

namespace IntegrationTests.Mocks;

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
