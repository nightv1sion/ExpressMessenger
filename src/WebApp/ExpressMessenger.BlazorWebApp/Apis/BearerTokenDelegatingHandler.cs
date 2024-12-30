using System.Net.Http.Headers;
using ExpressMessenger.BlazorWebApp.Authentication;

namespace ExpressMessenger.BlazorWebApp.Apis;

public sealed class BearerTokenDelegatingHandler(ITokenManager tokenManager) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var (_, token) = await tokenManager.GetAccessToken(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
} 