using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.CreateChat;

public sealed record CreateChatCommand(
    Guid CreatorId,
    IReadOnlyCollection<Guid> CompanionIds) : ICommand<Guid>;