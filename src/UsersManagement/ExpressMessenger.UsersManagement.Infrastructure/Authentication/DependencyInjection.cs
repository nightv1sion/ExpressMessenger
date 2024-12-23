using ExpressMessenger.UsersManagement.Application.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace ExpressMessenger.UsersManagement.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        JwtSettings jwtSettings = new();
        configuration.Bind(JwtSettings.Section, jwtSettings);
        
        services.AddSingleton(Options.Create(jwtSettings));
        return services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
    }
}