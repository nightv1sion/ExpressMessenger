namespace ExpressMessenger.Common.Infrastructure.Messaging;

public sealed class KafkaSettings
{
    public const string SectionName = "Kafka";
    
    public string Host { get; init; } = null!;

    public string Username { get; init; } = null!;

    public string Password { get; set; } = null!;
}