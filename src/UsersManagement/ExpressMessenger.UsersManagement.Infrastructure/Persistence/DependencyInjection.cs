using ExpressMessenger.UsersManagement.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using ExpressMessenger.UsersManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((_, options) =>
            options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention());
        return services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
}