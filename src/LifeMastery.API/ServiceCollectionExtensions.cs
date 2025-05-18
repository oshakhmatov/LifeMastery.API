using LifeMastery.Agenda;
using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance;
using LifeMastery.Health;
using LifeMastery.Infrastructure;

namespace LifeMastery.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var appSection = configuration.GetSection("LifeMastery");

        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(c => c.AssignableTo(typeof(ICommand<>)))
                .AsSelf()
                .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(ICommand<,>)))
                .AsSelf()
                .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo<ICommand>()) 
                .AsSelf()
                .WithScopedLifetime()
        );


        services.AddInfrastructure(appSection);

        services.AddJobsModule();
        services.AddWeightControlModule();
        services.AddFinanceModule();

        return services;
    }
}
