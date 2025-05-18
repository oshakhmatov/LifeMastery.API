using LifeMastery.Finance.Commands;
using LifeMastery.Finance.Services;
using LifeMastery.Finance.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Finance;

public static class FinanceModule
{
    public static IServiceCollection AddFinanceModule(this IServiceCollection services)
    {
        return services
            .AddTransient<EmailHandler>()
            .AddTransient<IExpenseParser, RaiffeisenExpenseParser>()
            .AddScoped<FinanceContextLoader>()
            .AddScoped<BudgetRuleResolver>()
            .AddScoped<EarningsResolver>()
            .AddScoped<FinanceStatisticsCalculator>()
            .AddScoped<GetFinanceData>();
    }
}
