using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(
    ApplicationDbContext context)
    : IUserRepository
{
    public async Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        await context.Set<User>()
            .AddAsync(user, cancellationToken);
    }
}