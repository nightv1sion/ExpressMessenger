using System.Text;
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
        JwtSettings jwtSettings = new();
        configuration.Bind(JwtSettings.Section, jwtSettings);
        
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
        services.AddAuthorization();
        
        return services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
    }
}