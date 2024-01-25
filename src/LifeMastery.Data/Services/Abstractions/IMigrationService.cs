namespace LifeMastery.Infrastructure.Services.Abstractions;

public interface IMigrationService : IDisposable
{
    public Task Migrate();
}
