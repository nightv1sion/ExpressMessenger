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
            .AddRefitClient<IUsersManagementApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apisSettings.UsersManagement.Url));

        return services;
    }
}