using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.GetChat;

internal sealed class GetChatHandler(
    IChatRepository chatRepository)
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

        return new ChatModel(
                chat.Id,
                chat.Members
                    .Where(member => member.UserId != request.UserId)
                    .Select(member => new ChatModel.CompanionModel(member.UserId))
                    .ToArray(),
                chat.Type);
    }
}