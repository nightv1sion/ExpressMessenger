using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> TryGetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}