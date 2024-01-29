using LifeMastery.Core.Modules.Finance.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LifeMastery.Application.HostedServices;

public sealed class EmailHandlerHostedService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public EmailHandlerHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var emailHandler = scope.ServiceProvider.GetRequiredService<EmailHandler>();

            await emailHandler.HandleInbox(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
        }
    }
}
