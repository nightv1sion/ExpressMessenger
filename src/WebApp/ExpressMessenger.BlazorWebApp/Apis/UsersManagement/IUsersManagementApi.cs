using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RegisterUser;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Apis.UsersManagement;

public interface IUsersManagementApi
{
    [Post("/users/register")]
    Task<RegisterUserResponse> RegisterUser(CancellationToken cancellationToken = default);
}