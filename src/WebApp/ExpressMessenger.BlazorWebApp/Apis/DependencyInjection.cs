using ExpressMessenger.BlazorWebApp.Notifications;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;
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
        
        services
            .AddRefitClient<IChatApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apisSettings.Chatting.Url));

        services.AddScoped<ChattingSignalRService>();
        
        services.AddTransient<BearerTokenDelegatingHandler>();
        
        return services;
    }
}