using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChats;

internal sealed class GetChatsHandler(
    IChatRepository chatRepository)
    : IQueryHandler<GetChatsQuery, IReadOnlyCollection<ChatModel>>
{
    public async Task<IReadOnlyCollection<ChatModel>> Handle(
        GetChatsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Chat> chats = await chatRepository.GetUserChats(
            request.UserId,
            cancellationToken);

        return chats
            .Select(x => new ChatModel(
                x.Id,
                x.Members
                    .Where(member => member.UserId != request.UserId)
                    .Select(member => new ChatModel.CompanionModel(member.UserId))
                    .ToArray(),
                x.Type))
            .ToArray();
    }
}