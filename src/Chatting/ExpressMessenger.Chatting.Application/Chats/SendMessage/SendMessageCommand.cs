using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.SendMessage;

public sealed record SendMessageCommand(
    Guid ChatId,
    Guid SenderId,
    string Text) : ICommand;