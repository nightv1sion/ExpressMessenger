using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(
    ApplicationDbContext context)
    : IUserRepository
{
    public async Task<uint> GetBiggestDisplayNumber(CancellationToken cancellationToken)
    {
        return await context.Set<User>().MaxAsync(x => x.DisplayNumber, cancellationToken);
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

    public async Task<IReadOnlyDictionary<Guid, uint>> GetDisplayNumbers(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .Where(x => userIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x.DisplayNumber, cancellationToken);
    }
}