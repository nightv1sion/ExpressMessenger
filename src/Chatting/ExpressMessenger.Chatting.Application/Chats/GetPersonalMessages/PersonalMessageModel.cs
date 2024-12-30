namespace ExpressMessenger.Chatting.Application.Chats.GetPersonalMessages;

public sealed record PersonalMessageModel(
    Guid Id,
    string Text,
    DateTimeOffset Sent);