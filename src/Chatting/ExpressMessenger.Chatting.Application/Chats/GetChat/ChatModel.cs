using ExpressMessenger.Chatting.Domain.ChatAggregate;

namespace ExpressMessenger.Chatting.Application.Chats.GetChat;

public sealed record ChatModel(
    Guid ChatId,
    ChatType Type,
    IReadOnlyCollection<ChatModel.CompanionModel> Companions,
    IReadOnlyCollection<ChatModel.MessageModel> Messages)
{
    public sealed record CompanionModel(
        Guid UserId,
        string UserNames);
    
    public sealed record MessageModel(
        Guid Id,
        string SenderUserName,
        string Text,
        bool IsMine,
        DateTimeOffset Sent);
};