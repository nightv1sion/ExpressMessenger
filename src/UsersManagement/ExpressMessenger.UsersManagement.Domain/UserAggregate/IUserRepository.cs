namespace ExpressMessenger.UsersManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task InsertAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> TryGetById(Guid id, CancellationToken cancellationToken);
}