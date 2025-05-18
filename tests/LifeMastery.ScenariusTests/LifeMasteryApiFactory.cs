using LifeMastery.Infrastructure.Data;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LifeMastery.ScenariusTests;

public class LifeMasteryApiFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName;

    public LifeMasteryApiFactory(string? dbName = null)
    {
        _dbName = dbName ?? Guid.NewGuid().ToString();
    }

    public string DatabaseName => _dbName;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
                options.UseLazyLoadingProxies();
            });

            var migrationServiceDescriptor = services.Single(d => d.ServiceType == typeof(IMigrationService));
            services.Remove(migrationServiceDescriptor);
            services.AddTransient<IMigrationService, MockMigrationService>();

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }
}