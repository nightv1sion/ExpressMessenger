namespace ExpressMessenger.Chatting.Application.Common;

public interface IUserInfoProvider
{
    Task<IReadOnlyDictionary<Guid, string>> GetUserNames(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken);
}