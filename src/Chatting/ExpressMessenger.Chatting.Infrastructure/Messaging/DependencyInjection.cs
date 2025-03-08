using ExpressMessenger.Chatting.Infrastructure.Messaging.Consumers;
using ExpressMessenger.Common.Infrastructure.Messaging;
using ExpressMessenger.SharedKernel.ApiContracts.Messaging.UserDeleted;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.Chatting.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection RegisterMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.RegisterMassTransitKafka(
            (configure) =>
            {
                configure.AddConsumer<UsersConsumer>();
            },
            (context, kafka) =>
            {
                kafka.TopicEndpoint<UserDeleted>(
                    "users-topic",
                    "chatting-group",
                    x =>
                    {
                        x.ConfigureConsumer<UsersConsumer>(context);
                    });
            },
            configuration);
    }
}