using Microsoft.AspNetCore.Routing;

namespace ExpressMessenger.Common.Api;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
