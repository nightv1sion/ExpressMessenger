using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUsersDisplayNumbers;

public sealed record GetUserNamesQuery(
    IReadOnlyCollection<Guid> UserIds)
    : IQuery<IReadOnlyDictionary<Guid, string>>;