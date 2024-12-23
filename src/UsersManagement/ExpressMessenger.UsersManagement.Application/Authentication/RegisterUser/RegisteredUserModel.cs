namespace ExpressMessenger.UsersManagement.Application.Authentication.RegisterUser;

public sealed record RegisteredUserModel(
    Guid UserId,
    string AccessToken,
    string RefreshToken);