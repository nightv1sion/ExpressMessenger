using ExpressMessenger.Common.Application;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.Common.Infrastructure.Messaging;

internal sealed class MessageProducer(
    IBusControl busControl,
    IServiceProvider serviceProvider) : IMessageProducer
{
    public async Task ProduceAsync<T>(T message, CancellationToken cancellationToken) where T : class
    {
        var producer = serviceProvider.GetRequiredService<ITopicProducer<T>>();
        await busControl.StartAsync(cancellationToken);
        await producer.Produce(message, cancellationToken);
        await busControl.StopAsync(cancellationToken);
    }
}