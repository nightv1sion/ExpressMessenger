namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RegisterUser;

public sealed record RegisterUserResponse(
    Guid UserId,
    string AccessToken,
    string RefreshToken);