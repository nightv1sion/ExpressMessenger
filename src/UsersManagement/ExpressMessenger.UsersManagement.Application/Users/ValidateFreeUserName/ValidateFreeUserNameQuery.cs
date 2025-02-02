using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Users.ValidateFreeUserName;

public sealed record ValidateFreeUserNameQuery(string UserName) : IQuery<bool>;