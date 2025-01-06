using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChat;

public sealed record GetChatQuery(
    Guid UserId,
    Guid ChatId)
    : IQuery<ChatModel>;