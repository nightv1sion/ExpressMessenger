using ExpressMessenger.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence.Repositories;

internal sealed class ChatRepository(
    ApplicationDbContext dbContext)
    : IChatRepository
{
    public async Task<IReadOnlyCollection<Chat>> GetUserChats(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Chat>()
            .Where(x => x.Members.Any(member => member.UserId == userId))
            .OrderByDescending(chat => chat.Messages
                .OrderBy(message => message.Sent)
                .Last()
                .Sent)
            .ThenByDescending(chat => chat.Created)
            .ToArrayAsync(cancellationToken);
    }

    public async Task InsertAsync(Chat chat, CancellationToken cancellationToken)
    {
        await dbContext.Set<Chat>().AddAsync(chat, cancellationToken);
    }

    public async Task<Chat?> TryGetById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Chat>()
            .Include(x => x.Messages.OrderBy(message => message.Sent))
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

    public void Remove(Chat chat)
    {
        dbContext.Set<Chat>().Remove(chat);
    }
}