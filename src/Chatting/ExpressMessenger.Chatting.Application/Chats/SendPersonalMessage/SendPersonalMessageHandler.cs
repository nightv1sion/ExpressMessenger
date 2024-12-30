using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.SendPersonalMessage;

internal sealed class SendPersonalMessageHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SendPersonalMessageCommand>
{
    public async Task Handle(SendPersonalMessageCommand request, CancellationToken cancellationToken)
    {
        Chat? chat = await chatRepository.TryGetPersonalBy(
            [request.SenderId, request.RecipientId],
            cancellationToken);

        if (chat is null)
        {
            chat = Chat.Personal(request.SenderId, request.RecipientId);
            await chatRepository.InsertAsync(chat, cancellationToken);
        }
        
        chat.SendMessage(request.Text, request.SenderId);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}