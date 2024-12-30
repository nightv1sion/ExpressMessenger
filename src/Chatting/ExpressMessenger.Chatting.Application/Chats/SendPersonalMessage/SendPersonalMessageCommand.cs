using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.SendPersonalMessage;

public sealed record SendPersonalMessageCommand(
    string Text,
    Guid SenderId,
    Guid RecipientId) : ICommand;