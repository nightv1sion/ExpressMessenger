using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserDto>;