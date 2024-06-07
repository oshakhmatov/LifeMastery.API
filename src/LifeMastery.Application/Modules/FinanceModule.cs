using LifeMastery.Core.Modules.Finance.Commands.EmailSubscriptions;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCategories;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCreationRules;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Commands.Info;
using LifeMastery.Core.Modules.Finance.Commands.Payments;
using LifeMastery.Core.Modules.Finance.Commands.RegularPayments;
using LifeMastery.Core.Modules.Finance.Queries;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;
using LifeMastery.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Application.Modules;

public static class FinanceModule
{
    public static IServiceCollection AddFinanceModule(this IServiceCollection services)
    {
        return services
            .AddTransient<EmailHandler>()
            .AddTransient<IExpenseParser, RaiffeisenExpenseParser>()
            .AddTransient<IEmailProvider, EmailProvider>()
            .AddScoped<IExpenseRepository, ExpenseRepository>()
            .AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>()
            .AddScoped<IRegularPaymentRepository, RegularPaymentRepository>()
            .AddScoped<IEmailSubscriptionRepository, EmailSubscriptionRepository>()
            .AddScoped<IFinanceInfoRepository, FinanceInfoRepository>()
            .AddScoped<GetFinanceData>()
            .AddScoped<PutExpense>()
            .AddScoped<UpdateExpenses>()
            .AddScoped<LoadExpenses>()
            .AddScoped<RemoveExpense>()
            .AddScoped<PutExpenseCategory>()
            .AddScoped<RemoveExpenseCategory>()
            .AddScoped<PutRegularPayment>()
            .AddScoped<RemoveRegularPayment>()
            .AddScoped<PutEmailSubscription>()
            .AddScoped<PutExpenseCreationRule>()
            .AddScoped<RemoveExpenseCreationRule>()
            .AddScoped<RemoveEmailSubscription>()
            .AddScoped<PutPayment>()
            .AddScoped<RemovePayment>()
            .AddScoped<PutFinanceInfo>();
    }
}
