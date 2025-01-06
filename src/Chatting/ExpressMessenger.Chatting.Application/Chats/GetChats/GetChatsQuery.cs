using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChats;

public sealed record GetChatsQuery(
    Guid UserId)
    : IQuery<IReadOnlyCollection<ChatModel>>;