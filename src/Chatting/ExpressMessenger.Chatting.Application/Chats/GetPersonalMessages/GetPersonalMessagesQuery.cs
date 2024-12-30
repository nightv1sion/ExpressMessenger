using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetPersonalMessages;

public sealed record GetPersonalMessagesQuery(
    Guid RequestUserId,
    Guid CompanionUserId) : IQuery<IReadOnlyCollection<PersonalMessageModel>>;