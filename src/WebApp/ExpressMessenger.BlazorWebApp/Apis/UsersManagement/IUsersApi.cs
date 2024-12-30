using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.GetUser;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Apis.UsersManagement;

public interface IUsersApi
{
    [Get("/users")]
    Task<GetUserResponse> GetUser(
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
}