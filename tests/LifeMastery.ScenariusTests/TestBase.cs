using LifeMastery.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Scenarius;

namespace LifeMastery.ScenariusTests;

public abstract class TestBase
{
    private static readonly ScenarioOptions DefaultScenarioOptions = new()
    {
        ErrorMessagePath = "message"
    };

    protected readonly LifeMasteryApiFactory Factory;

    protected TestBase()
    {
        var dbName = Guid.NewGuid().ToString();
        Factory = new LifeMasteryApiFactory(dbName);
    }

    protected async Task RunScenario(Func<ScenarioBuilder, ScenarioBuilder> configure)
    {
        var client = Factory.CreateClient();
        var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var builder = new ScenarioBuilder(client, db, DefaultScenarioOptions);
        var scenario = configure(builder);

        await scenario.ExecuteAsync();
    }

}