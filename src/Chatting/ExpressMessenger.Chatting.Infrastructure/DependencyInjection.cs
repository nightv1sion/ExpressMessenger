using ExpressMessenger.Chatting.Infrastructure.Apis;
using ExpressMessenger.Chatting.Infrastructure.Persistence;
using ExpressMessenger.Common.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.Chatting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .RegisterJwtAuthentication(configuration, nameof(JwtSettings))
            .RegisterApis(configuration)
            .RegisterPersistence(configuration);
    }
}