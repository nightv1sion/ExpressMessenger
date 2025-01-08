using ExpressMessenger.Chatting.Application.Common;
using ExpressMessenger.Common.Infrastructure;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ExpressMessenger.Chatting.Infrastructure.Apis;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApis(this IServiceCollection services, IConfiguration configuration)
    {
        var apisSettings = new ApisSettings();
        configuration.Bind(ApisSettings.SectionName, apisSettings);
        services.AddSingleton(apisSettings);

        services.AddTransient<BearerTokenDelegatingHandler>();
        
        services
            .AddScoped<IUserInfoProvider, UserInfoProvider>()
            .AddRefitClient<IUsersApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apisSettings.UsersManagement.Url))
            .AddHttpMessageHandler<BearerTokenDelegatingHandler>();
        
        return services;
    }
}