namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RefreshToken;

public sealed record RefreshTokenRequest(
    Guid UserId,
    string AccessToken,
    string RefreshToken);