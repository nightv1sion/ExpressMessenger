using ExpressMessenger.Chatting.Application.Common;
using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChats;

internal sealed class GetChatsHandler(
    IChatRepository chatRepository,
    IUserInfoProvider userInfoProvider)
    : IQueryHandler<GetChatsQuery, IReadOnlyCollection<ChatModel>>
{
    public async Task<IReadOnlyCollection<ChatModel>> Handle(
        GetChatsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Chat> chats = await chatRepository.GetUserChats(
            request.UserId,
            cancellationToken);
        
        if (chats.Count == 0)
        {
            return Array.Empty<ChatModel>();
        }
        
        HashSet<Guid> userIds = chats
            .SelectMany(x => x.Members.Select(member => member.UserId))
            .ToHashSet();
        
        IReadOnlyDictionary<Guid, string> userNames = await userInfoProvider.GetUserNames(
            userIds,
            cancellationToken);

        return chats
            .Select(x => new ChatModel(
                x.Id,
                x.Members
                    .Where(member => member.UserId != request.UserId)
                    .Select(member => new ChatModel.CompanionModel(
                        member.UserId,
                        userNames[member.UserId]))
                    .ToArray(),
                x.Type))
            .ToArray();
    }
}