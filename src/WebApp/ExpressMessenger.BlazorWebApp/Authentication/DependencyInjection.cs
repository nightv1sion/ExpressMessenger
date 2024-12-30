namespace ExpressMessenger.BlazorWebApp.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAuthentication(
        this IServiceCollection services)
    {
        return services.AddTransient<ITokenManager, TokenManager>();
    }
}