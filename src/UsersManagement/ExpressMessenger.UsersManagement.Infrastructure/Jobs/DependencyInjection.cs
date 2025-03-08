using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;

namespace ExpressMessenger.UsersManagement.Infrastructure.Jobs;

public static class DependencyInjection
{
    public static IServiceCollection RegisterJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            JobKey jobKey = new("RemoveOutdatedUsersJob");
            q.AddJob<RemoveOutdatedUsersJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("RemoveOutdatedUsersJob-trigger")
                .WithCronSchedule("0 * * ? * *"));
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        
        return services;
    }
}