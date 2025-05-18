using LifeMastery.Agenda.Repositories;
using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Repositories;
using LifeMastery.Finance.Services.Abstractions;
using LifeMastery.Health.Repositories;
using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Data.Repositories.AgendaRepositories;
using LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;
using LifeMastery.Infrastructure.Data.Repositories.HealthRepositories;
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

        AddRepositories(services);
        services.AddTransient<IEmailProvider, EmailProvider>();
        services.AddSingleton<IAppCultureProvider, RsCultureProvider>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContext<IUnitOfWork, AppDbContext>();
        services.AddTransient<IMigrationService, MigrationService>();

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        services.AddScoped<IRegularPaymentRepository, RegularPaymentRepository>();
        services.AddScoped<IEarningRepository, EarningRepository>();
        services.AddScoped<IFamilyBudgetRuleRepository, FamilyBudgetRuleRepository>();
        services.AddScoped<IWeightRecordRepository, WeightRecordRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
    }
}
