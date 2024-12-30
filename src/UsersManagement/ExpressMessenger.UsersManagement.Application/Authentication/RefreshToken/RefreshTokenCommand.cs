using ExpressMessenger.Common.Application;

namespace ExpressMessenger.UsersManagement.Application.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(
    Guid UserId,
    string AccessToken,
    string RefreshToken) : ICommand<RefreshTokenModel>;