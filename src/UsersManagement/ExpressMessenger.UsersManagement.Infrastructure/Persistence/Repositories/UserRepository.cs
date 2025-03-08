using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(
    ApplicationDbContext context)
    : IUserRepository
{
    public async Task<User?> TryGetBy(string userName, CancellationToken cancellationToken)
    {
        return await context.Set<User>().FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
    }

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

    public async Task<IReadOnlyDictionary<Guid, string>> GetUserNames(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .Where(x => userIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.UserName, cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> SearchBy(
        IReadOnlyCollection<string>? userNames = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<User> query = context.Set<User>();

        if (userNames is not null)
        {
            query = query.Where(x => userNames.Contains(x.UserName));
        }
        
        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<User>> GetBy(
        DateTimeOffset refreshTokenExpiredBefore,
        CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .Where(x => x.RefreshTokenExpired < refreshTokenExpiredBefore)
            .ToArrayAsync(cancellationToken);
    }

    public void Remove(User user)
    {
        context.Set<User>().Remove(user);
    }
}