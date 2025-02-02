namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUser;

public sealed record GetUserResponse(
    Guid UserId,
    string UserName);