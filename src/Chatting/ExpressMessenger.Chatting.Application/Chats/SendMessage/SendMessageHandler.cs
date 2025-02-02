using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Chats.SendMessage;

internal sealed class SendMessageHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SendMessageCommand>
{
    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        Chat? chat = await chatRepository.TryGetById(
            request.ChatId,
            cancellationToken);

        if (chat is null || chat.Members.All(member => member.UserId != request.SenderId))
        {
            throw new ChatNotFoundException(request.SenderId, request.ChatId);
        }
        
        chat.SendMessage(request.Text, request.SenderId);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}