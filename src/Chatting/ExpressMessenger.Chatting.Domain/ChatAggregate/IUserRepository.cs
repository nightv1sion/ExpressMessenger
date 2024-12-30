namespace ExpressMessenger.Chatting.Domain.ChatAggregate;

public interface IChatRepository
{
    Task InsertAsync(Chat chat, CancellationToken cancellationToken);
    
    Task<Chat?> TryGetById(Guid id, CancellationToken cancellationToken);

    Task<Chat?> TryGetPersonalBy(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken);
}