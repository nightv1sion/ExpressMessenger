using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RefreshToken;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RegisterUser;
using Refit;

namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication;

public interface IAuthenticationApi
{
    [Post("/authentication/register")]
    Task<RegisterUserResponse> RegisterUser(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    [Post("/authentication/refresh-token")]
    Task<IApiResponse<RefreshTokenResponse>> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default);
}