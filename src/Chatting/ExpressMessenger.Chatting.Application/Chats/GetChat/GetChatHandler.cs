using ExpressMessenger.Chatting.Application.Common;
using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChat;

internal sealed class GetChatHandler(
    IChatRepository chatRepository,
    IUserInfoProvider userInfoProvider)
    : IQueryHandler<GetChatQuery, ChatModel>
{
    public async Task<ChatModel> Handle(
        GetChatQuery request,
        CancellationToken cancellationToken)
    {
        Chat? chat = await chatRepository.TryGetById(
            request.ChatId,
            cancellationToken);

        if (chat is null || !chat.Members.Select(member => member.UserId).Contains(request.UserId))
        {
            throw new ChatNotFoundException(request.UserId, request.ChatId);
        }
        
        HashSet<Guid> userIds = chat.Members
            .Select(member => member.UserId)
            .ToHashSet();
        
        IReadOnlyDictionary<Guid, uint> displayNumbers = await userInfoProvider.GetDisplayNumbers(
            userIds,
            cancellationToken);

        return new ChatModel(
                chat.Id,
                chat.Members
                    .Where(member => member.UserId != request.UserId)
                    .Select(member => new ChatModel.CompanionModel(
                        member.UserId,
                        displayNumbers[member.UserId]))
                    .ToArray(),
                chat.Type);
    }
}