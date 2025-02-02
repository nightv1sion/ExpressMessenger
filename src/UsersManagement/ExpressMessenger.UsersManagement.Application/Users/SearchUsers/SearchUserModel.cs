namespace ExpressMessenger.UsersManagement.Application.Users.SearchUsers;

public sealed record SearchUserModel(
    Guid UserId,
    string UserName);