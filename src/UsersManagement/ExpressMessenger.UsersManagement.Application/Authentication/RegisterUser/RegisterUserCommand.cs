using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Authentication.RegisterUser;

public sealed record RegisterUserCommand(string UserName) : ICommand<RegisteredUserModel>;