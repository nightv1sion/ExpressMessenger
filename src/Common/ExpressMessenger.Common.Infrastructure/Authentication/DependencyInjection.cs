using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpressMessenger.Common.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection RegisterJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string jwtSettingsSection)
    {
        JwtSettings jwtSettings = new();
        configuration.Bind(jwtSettingsSection, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        
        services
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });
        return services.AddAuthorization();
    }
}