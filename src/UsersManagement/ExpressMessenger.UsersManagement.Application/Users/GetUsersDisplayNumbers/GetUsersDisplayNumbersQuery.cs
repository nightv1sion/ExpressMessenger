using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUsersDisplayNumbers;

public sealed record GetUsersDisplayNumbersQuery(
    IReadOnlyCollection<Guid> UserIds)
    : IQuery<IReadOnlyDictionary<Guid, uint>>;