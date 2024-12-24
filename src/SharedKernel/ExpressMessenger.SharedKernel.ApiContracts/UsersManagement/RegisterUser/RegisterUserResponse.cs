namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RegisterUser;

public sealed record RegisterUserResponse(
    Guid UserId,
    string AccessToken,
    string RefreshToken);