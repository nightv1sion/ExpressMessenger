namespace ExpressMessenger.UsersManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task<User?> TryGetBy(string userName, CancellationToken cancellationToken);
    
    Task InsertAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> TryGetById(Guid id, CancellationToken cancellationToken);
    
    Task<IReadOnlyDictionary<Guid, string>> GetUserNames(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<User>> SearchBy(
        IReadOnlyCollection<string>? userNames = null,
        CancellationToken cancellationToken = default);
}