using IntegrationTests.Mocks;
using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public class WebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            services.Remove(dbContextDescriptor);
            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("TestDb"));

            var migrationServiceDescriptor = services.Single(d => d.ServiceType == typeof(IMigrationService));
            services.Remove(migrationServiceDescriptor);
            services.AddTransient<IMigrationService, MockMigrationService>();
        });

        builder.UseEnvironment("Development");
    }
}