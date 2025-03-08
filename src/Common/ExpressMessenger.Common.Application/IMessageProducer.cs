namespace ExpressMessenger.Common.Application;

public interface IMessageProducer
{
    Task ProduceAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}