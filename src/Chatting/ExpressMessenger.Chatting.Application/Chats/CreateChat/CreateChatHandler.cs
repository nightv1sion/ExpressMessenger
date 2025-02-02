using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.CreateChat;

internal sealed class CreateChatHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateChatCommand, Guid>
{
    public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        Chat chat = Chat.Group(request.CreatorId, request.CompanionIds);
        
        await chatRepository.InsertAsync(chat, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return chat.Id;
    }
}