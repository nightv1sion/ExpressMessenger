using ExpressMessenger.Chatting.Application.Notifications;
using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.CreateChat;

internal sealed class CreateChatHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork,
    IUserNotifier userNotifier) : ICommandHandler<CreateChatCommand, Guid>
{
    public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        Chat chat = Chat.Group(request.CreatorId, request.CompanionIds);
        
        await chatRepository.InsertAsync(chat, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await userNotifier.NotifyUsersAboutNewChat(
            request.CompanionIds,
            cancellationToken);
        
        return chat.Id;
    }
}