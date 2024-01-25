using AutoFixture;
using LifeMastery.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Tests;

public abstract class TestBase : IClassFixture<WebAppFactory>, IDisposable
{
    private protected readonly Fixture fixture = new();
    private protected readonly WebAppFactory factory;

    public TestBase(WebAppFactory factory)
    {
        this.factory = factory;
    }

    public void Dispose()
    {
        var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.EnsureDeleted();
    }
}
