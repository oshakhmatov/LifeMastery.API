using LifeMastery.Application.HostedServices;
using LifeMastery.Application.HostedServices.Options;
using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.Queries;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;
using LifeMastery.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Application.Modules;

public static class FinanceModule
{
    public static IServiceCollection AddFinanceModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<EmailHandlerServiceOptions>(configuration.GetSection(nameof(EmailHandlerServiceOptions)))
            .AddHostedService<EmailHandlerHostedService>()
            .AddTransient<EmailHandler>()
            .AddTransient<ExpenseParser>()
            .AddTransient<IExpenseCategoryResolver, ExpenseCategoryResolver>()
            .AddTransient<IEmailProvider, EmailProvider>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>()
            .AddScoped<IRegularPaymentRepository, RegularPaymentRepository>()
            .AddScoped<IEmailSubscriptionRepository, EmailSubscriptionRepository>()
            .AddScoped<GetFinanceData>()
            .AddScoped<PutExpense>()
            .AddScoped<RemoveExpense>()
            .AddScoped<PutExpenseCategory>()
            .AddScoped<RemoveExpenseCategory>()
            .AddScoped<PutRegularPayment>()
            .AddScoped<RemoveRegularPayment>();
    }
}
