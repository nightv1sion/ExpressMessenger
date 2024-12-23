using ExpressMessenger.UsersManagement.Infrastructure.Authentication;
using ExpressMessenger.UsersManagement.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.UsersManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .RegisterPersistence(configuration)
            .RegisterAuthentication(configuration);
    }
}