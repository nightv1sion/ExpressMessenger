namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RefreshToken;

public sealed record RefreshTokenRequest(
    Guid UserId,
    string AccessToken,
    string RefreshToken);