namespace ExpressMessenger.BlazorWebApp.Authentication;

public interface ITokenManager
{
    Task SetToken(
        Guid userId,
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken = default);
    
    Task<(bool, string?)> GetAccessToken(CancellationToken cancellationToken = default);

    string ToBearer(string token);
}