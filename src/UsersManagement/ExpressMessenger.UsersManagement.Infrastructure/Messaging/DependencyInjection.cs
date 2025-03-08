using ExpressMessenger.Common.Infrastructure.Messaging;
using ExpressMessenger.SharedKernel.ApiContracts.Messaging.UserDeleted;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.UsersManagement.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection RegisterMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.RegisterMassTransitKafka(rider =>
            {
                rider.AddProducer<UserDeleted>("users-topic");
            },
            (_, _) => {},
            configuration);
    }
}