using ExpressMessenger.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence.Repositories;

internal sealed class ChatRepository(
    ApplicationDbContext dbContext) : IChatRepository
{
    public async Task InsertAsync(Chat chat, CancellationToken cancellationToken)
    {
        await dbContext.Set<Chat>().AddAsync(chat, cancellationToken);
    }

    public async Task<Chat?> TryGetById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Chat>()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Chat?> TryGetPersonalBy(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Chat>()
            .Where(x => x.Type == ChatType.Personal)
            .Where(x => x.Members.All(member => userIds.Contains(member.UserId)))
            .Where(x => x.Members.Count == userIds.Count)
            .SingleOrDefaultAsync(cancellationToken);
    }
}