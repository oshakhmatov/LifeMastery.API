using LifeMastery.Health.Services;
using LifeMastery.Health.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Health;

public static class WeightControlModule
{
    public static IServiceCollection AddWeightControlModule(this IServiceCollection services)
    {
        return services
            .AddScoped<IBodyMassIndexService, BodyMassIndexService>()
            .AddScoped<IHealthService, HealthService>()
            .AddScoped<IStatisticService, StatisticService>();
    }
}
