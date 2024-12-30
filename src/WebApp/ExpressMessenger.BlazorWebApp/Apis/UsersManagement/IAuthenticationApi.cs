using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RefreshToken;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RegisterUser;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Apis.UsersManagement;

public interface IAuthenticationApi
{
    [Post("/authentication/register")]
    Task<RegisterUserResponse> RegisterUser(CancellationToken cancellationToken = default);

    [Post("/authentication/refresh-token")]
    Task<IApiResponse<RefreshTokenResponse>> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default);
}