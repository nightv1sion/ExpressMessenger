using ExpressMessenger.Common.Domain;

namespace ExpressMessenger.Chatting.Domain.ChatAggregate;

public sealed class Chat : AggregateRoot
{
    private readonly List<Message> _messages = new();
    private readonly List<Member> _members = new();
    
    public ChatType Type { get; private init; }
    
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public IReadOnlyCollection<Member> Members => _members.AsReadOnly();
    
    public void SendMessage(
        string text,
        Guid senderId)
    {
        _messages.Add(Message.Create(
            text,
            Id,
            senderId));
    }

    public void AddMember(Guid userId)
    {
        _members.Add(Member.Create(userId, Id));
    }
    
    public void AddMembers(IReadOnlyCollection<Guid> userIds)
    {
        _members.AddRange(userIds.Select(userId => Member.Create(userId, Id)));
    }

    public static Chat Personal(
        Guid initiatorId,
        Guid companionId)
    {
        Chat chat = new()
        {
            Id = Guid.NewGuid(),
            Type = ChatType.Personal,
        };

        chat.AddMember(initiatorId);
        chat.AddMember(companionId);

        return chat;
    }
    public static Chat Group(
        Guid initiatorId,
        IReadOnlyCollection<Guid> companionIds)
    {
        Chat chat = new()
        {
            Id = Guid.NewGuid(),
            Type = ChatType.Personal,
        };

        chat.AddMember(initiatorId);
        chat.AddMembers(companionIds);

        return chat;
    }
    

    private Chat() { }
}