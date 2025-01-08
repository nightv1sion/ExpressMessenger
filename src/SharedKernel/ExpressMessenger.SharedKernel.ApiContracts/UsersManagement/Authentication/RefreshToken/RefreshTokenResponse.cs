namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RefreshToken;

public sealed record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken);