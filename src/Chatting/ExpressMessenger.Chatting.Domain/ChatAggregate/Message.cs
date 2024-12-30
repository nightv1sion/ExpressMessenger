using ExpressMessenger.Common.Domain;

namespace ExpressMessenger.Chatting.Domain.ChatAggregate;

public sealed class Message : Entity
{
    public string Text { get; private init; } = null!;

    public Guid ChatId { get; private init; }

    public Guid SenderId { get; private init; }

    public DateTimeOffset Sent { get; private init; }

    public static Message Create(
        string text,
        Guid chatId,
        Guid senderId)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            Text = text,
            ChatId = chatId,
            SenderId = senderId,
            Sent = DateTimeOffset.UtcNow,
        };
    }
    
    private Message() { }
}