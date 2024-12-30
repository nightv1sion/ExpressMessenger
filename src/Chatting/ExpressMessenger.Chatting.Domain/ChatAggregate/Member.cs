namespace ExpressMessenger.Chatting.Domain.ChatAggregate;

public sealed record Member
{
    public Guid UserId { get; private init; }

    public Guid ChatId { get; private init; }
    
    private Member() { }

    public static Member Create(Guid userId, Guid chatId)
    {
        return new Member
        {
            UserId = userId,
            ChatId = chatId
        };
    }
}