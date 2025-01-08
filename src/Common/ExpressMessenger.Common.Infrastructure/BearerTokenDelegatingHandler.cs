using Microsoft.AspNetCore.Http;

namespace ExpressMessenger.Common.Infrastructure;

public sealed class BearerTokenDelegatingHandler(
    IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = httpContextAccessor.HttpContext!.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.TryAddWithoutValidation("Authorization", token);
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}