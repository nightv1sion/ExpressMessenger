using System.Text.Json;
using ExpressMessenger.Common.Application;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExpressMessenger.Common.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection RegisterMassTransitKafka(
        this IServiceCollection services,
        Action<IRiderRegistrationConfigurator> riderConfigurator,
        Action<IRiderRegistrationContext,IKafkaFactoryConfigurator> kafkaConfigurator,
        IConfiguration configuration)
    {
        var kafkaSettings = new KafkaSettings();
        configuration.Bind(KafkaSettings.SectionName, kafkaSettings);
        services.AddSingleton(Options.Create(kafkaSettings));
        
        return services
            .AddMassTransit(x =>
            {
                x.UsingInMemory((context, config) =>
                {
                    config.ConfigureEndpoints(context);
                });
                
                x.AddRider(rider =>
                {
                    riderConfigurator(rider);
                    rider.UsingKafka((context, kafka) =>
                    {
                        kafka.Host(kafkaSettings.Host, configure =>
                        {
                            configure.UseSasl(sasl =>
                            {
                                sasl.Username = kafkaSettings.Username;
                                sasl.Password = kafkaSettings.Password;
                            });
                            
                        });
                        
                        kafkaConfigurator.Invoke(context, kafka);
                    });
                });
            })
            .AddScoped<IMessageProducer, MessageProducer>();
    }
}