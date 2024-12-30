using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetPersonalMessages;

internal sealed class GetPersonalMessagesHandler(
    IChatRepository chatRepository)
    : IQueryHandler<GetPersonalMessagesQuery, IReadOnlyCollection<PersonalMessageModel>>
{
    public async Task<IReadOnlyCollection<PersonalMessageModel>> Handle(
        GetPersonalMessagesQuery request,
        CancellationToken cancellationToken)
    {
        Chat? chat = await chatRepository.TryGetPersonalBy(
            [request.RequestUserId, request.CompanionUserId],
            cancellationToken);

        if (chat is null)
        {
            return Array.Empty<PersonalMessageModel>();
        }

        return chat.Messages.Select(m => new PersonalMessageModel(
                m.Id,
                m.Text,
                m.Sent))
            .ToArray();
    }
}