using ExpressMessenger.Chatting.Domain.ChatAggregate;

namespace ExpressMessenger.Chatting.Application.Chats.GetChats;

public sealed record ChatModel(
    Guid ChatId,
    IReadOnlyCollection<ChatModel.CompanionModel> Companions,
    ChatType Type)
{
    public sealed record CompanionModel(
        Guid UserId,
        string UserName);
};