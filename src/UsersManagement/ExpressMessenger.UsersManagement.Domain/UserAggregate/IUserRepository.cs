namespace ExpressMessenger.UsersManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task<uint> GetBiggestDisplayNumber(CancellationToken cancellationToken);
    
    Task InsertAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> TryGetById(Guid id, CancellationToken cancellationToken);
    
    Task<IReadOnlyDictionary<Guid, uint>> GetDisplayNumbers(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken);
}