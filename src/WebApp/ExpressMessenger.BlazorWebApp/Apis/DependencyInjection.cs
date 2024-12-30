using ExpressMessenger.BlazorWebApp.Apis.UsersManagement;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Apis;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApis(this IServiceCollection services, IConfiguration configuration)
    {
        var apisSettings = new ApisSettings();
        configuration.Bind(ApisSettings.SectionName, apisSettings);
        services.AddSingleton(apisSettings);

        services
            .AddRefitClient<IAuthenticationApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apisSettings.UsersManagement.Url));
        
        services
            .AddRefitClient<IUsersApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apisSettings.UsersManagement.Url));

        services.AddTransient<BearerTokenDelegatingHandler>();
        
        return services;
    }
}