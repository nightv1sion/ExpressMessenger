using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Users.SearchUsers;

public sealed record SearchUsersQuery(string[]? UserNames) : IQuery<IReadOnlyCollection<SearchUserModel>>;