using LifeMastery.Core;
using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Options;
using LifeMastery.Infrastructure.Services;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(nameof(DbOptions)));
        services.Configure<EmailProviderOptions>(configuration.GetSection(nameof(EmailProviderOptions)));

        services.AddDbContext<IUnitOfWork, AppDbContext>();
        services.AddTransient<IMigrationService, MigrationService>();

        return services;
    }
}
