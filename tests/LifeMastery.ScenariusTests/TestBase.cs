using LifeMastery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.ScenariusTests;

public abstract class TestBase
{
    private readonly string dbName = Guid.NewGuid().ToString();

    private static readonly ScenarioOptions DefaultScenarioOptions = new()
    {
        ErrorMessagePath = "message"
    };

    protected readonly LifeMasteryApiFactory Factory;

    protected TestBase()
    {
        Factory = new LifeMasteryApiFactory(dbName);
    }

    protected async Task RunScenario(Func<ScenarioBuilder, ScenarioBuilder> configure)
    {
        var client = Factory.CreateClient();
        var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var builder = new ScenarioBuilder(
            client,
            () => new AppDbContext(new DbContextOptionsBuilder().UseInMemoryDatabase(dbName).UseLazyLoadingProxies().Options), 
            DefaultScenarioOptions);
        var scenario = configure(builder);

        await scenario.ExecuteAsync();
    }

}