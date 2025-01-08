namespace ExpressMessenger.Chatting.Domain.ChatAggregate;

public interface IChatRepository
{
    Task<IReadOnlyCollection<Chat>> GetUserChats(Guid userId, CancellationToken cancellationToken);
    
    Task InsertAsync(Chat chat, CancellationToken cancellationToken);
    
    Task<Chat?> TryGetById(Guid id, CancellationToken cancellationToken);

    Task<Chat?> TryGetPersonalBy(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken);
}