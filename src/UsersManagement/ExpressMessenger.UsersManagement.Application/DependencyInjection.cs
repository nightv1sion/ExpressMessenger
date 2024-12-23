using ExpressMessenger.Common.Application;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.UsersManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(
        this IServiceCollection services)
    {
        return services
            .RegisterMediator(AssemblyReference.Assembly);
    }
}