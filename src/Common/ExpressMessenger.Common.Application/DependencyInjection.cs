using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.Common.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterMediator(
        this IServiceCollection services,
        Assembly assembly)
    {
        return services
            .AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
    }
}