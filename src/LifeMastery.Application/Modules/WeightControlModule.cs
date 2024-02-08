using LifeMastery.Core.Modules.WeightControl.Commands;
using LifeMastery.Core.Modules.WeightControl.Queries;
using LifeMastery.Core.Modules.WeightControl.Repositories;
using LifeMastery.Core.Modules.WeightControl.Services;
using LifeMastery.Core.Modules.WeightControl.Services.Abstractions;
using LifeMastery.Infrastructure.Data.Repositories.WeightControlRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Application.Modules;

public static class WeightControlModule
{
    public static IServiceCollection AddWeightControlModule(this IServiceCollection services)
    {
        return services
            .AddScoped<IBodyMassIndexService, BodyMassIndexService>()
            .AddScoped<IHealthService, HealthService>()
            .AddScoped<IStatisticService, StatisticService>()
            .AddScoped<IHealthInfoRepository, HealthInfoRepository>()
            .AddScoped<IWeightRecordRepository, WeightRecordRepository>()
            .AddScoped<GetWeightControlData>()
            .AddScoped<PutHealthInfo>()
            .AddScoped<PutWeightRecord>()
            .AddScoped<RemoveWeightRecord>();
    }
}
