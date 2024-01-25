using LifeMastery.Core.Modules.Jobs.Commands;
using LifeMastery.Core.Modules.Jobs.Queries;
using LifeMastery.Core.Modules.Jobs.Repositories;
using LifeMastery.Infrastructure.Data.Repositories.JobsRepocitories;
using Microsoft.Extensions.DependencyInjection;

namespace LifeMastery.Application.Modules;

public static class JobsModule
{
    public static IServiceCollection AddJobsModule(this IServiceCollection services)
    {
        return services
            .AddScoped<IJobRepository, JobRepository>()
            .AddScoped<AddJob>()
            .AddScoped<DeleteJob>()
            .AddScoped<UpdateJob>()
            .AddScoped<GetJobs>();
    }
}