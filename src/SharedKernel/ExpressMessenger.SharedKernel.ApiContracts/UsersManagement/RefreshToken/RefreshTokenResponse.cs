namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RefreshToken;

public sealed record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken);