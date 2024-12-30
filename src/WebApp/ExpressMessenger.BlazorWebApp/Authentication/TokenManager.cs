using System.IdentityModel.Tokens.Jwt;
using ExpressMessenger.BlazorWebApp.Apis.UsersManagement;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.RefreshToken;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Authentication;

public sealed class TokenManager(
    ProtectedLocalStorage localStorage,
    IAuthenticationApi authenticationApi) : ITokenManager
{
    public async Task SetToken(
        Guid userId,
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        JwtSecurityToken jwtAccessToken = new(accessToken);
        DateTimeOffset accessTokenExpiration = jwtAccessToken.ValidTo.ToUniversalTime();

        UserLocalStorageInfo userInfo = new(
            userId,
            accessToken,
            accessTokenExpiration,
            refreshToken);
            
        await localStorage.SetAsync("user-info", userInfo);
    }

    public async Task<(bool, string?)> GetAccessToken(CancellationToken cancellationToken)
    {
        ProtectedBrowserStorageResult<UserLocalStorageInfo> localStorageUserInfo = await localStorage
            .GetAsync<UserLocalStorageInfo>("user-info");
    
        if (!localStorageUserInfo.Success || localStorageUserInfo.Value is null)
        {
            return (false, null);
        }

        UserLocalStorageInfo userInfo = localStorageUserInfo.Value;

        if (userInfo.AccessTokenExpiration >= DateTimeOffset.UtcNow)
        {
            return (true, userInfo.AccessToken);
        }

        IApiResponse<RefreshTokenResponse> apiResponse = await authenticationApi.RefreshToken(
            new RefreshTokenRequest(
                userInfo.UserId,
                userInfo.AccessToken,
                userInfo.RefreshToken),
            cancellationToken);

        if (!apiResponse.IsSuccessStatusCode)
        {
            await localStorage.DeleteAsync("user-info");
            return (false, null);
        }

        await SetToken(
            userInfo.UserId,
            apiResponse.Content!.AccessToken,
            apiResponse.Content!.RefreshToken,
            cancellationToken);
            
        return (true, apiResponse.Content!.AccessToken);
    }
    
    sealed record UserLocalStorageInfo(
        Guid UserId,
        string AccessToken,
        DateTimeOffset AccessTokenExpiration,
        string RefreshToken);
}