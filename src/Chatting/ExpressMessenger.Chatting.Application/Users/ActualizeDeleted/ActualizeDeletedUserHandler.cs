using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Users.ActualizeDeleted;

internal sealed class ActualizeDeletedUserHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ActualizeDeletedUserCommand>
{
    public async Task Handle(
        ActualizeDeletedUserCommand request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Chat> chats = await chatRepository.GetUserChats(
            request.UserId,
            cancellationToken);
        
        foreach (Chat chat in chats)
        {
            chat.RemoveMember(request.UserId);
            chat.RemoveMemberMessages(request.UserId);
            
            if (chat.Members.Count <= 1)
            {
                chatRepository.Remove(chat);
            }
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}