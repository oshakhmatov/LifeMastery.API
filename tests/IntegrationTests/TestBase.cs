using AutoFixture;
using AutoFixture.AutoMoq;
using Common.AutoFixture;
using LifeMastery.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class TestBase : IClassFixture<WebAppFactory>, IDisposable
{
    private protected readonly IFixture fixture;
    private protected readonly WebAppFactory factory;

    public TestBase(WebAppFactory factory)
    {
        this.factory = factory;

        fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Customizations.Add(new DateOnlySpecimenBuilder());
    }

    public void Dispose()
    {
        var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.EnsureDeleted();
    }
}
