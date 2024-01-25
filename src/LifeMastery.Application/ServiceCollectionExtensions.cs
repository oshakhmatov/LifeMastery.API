using LifeMastery.Application.Modules;
using LifeMastery.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var appSection = configuration.GetSection("LifeMastery");

        services.AddInfrastructure(appSection);

        services.AddJobsModule();
        services.AddWeightControlModule();
        services.AddFinanceModule(appSection);

        return services;
    }
}
