namespace ExpressMessenger.Chatting.Application.Common;

public interface IUserInfoProvider
{
    Task<IReadOnlyDictionary<Guid, uint>> GetDisplayNumbers(
        IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken);
}