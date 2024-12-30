using System.Text;
using ExpressMessenger.Common.Infrastructure.Authentication;
using ExpressMessenger.UsersManagement.Application.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace ExpressMessenger.UsersManagement.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterJwtAuthentication(configuration, nameof(JwtSettings));
        
        return services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
    }
}