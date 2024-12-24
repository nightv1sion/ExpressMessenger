using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.GetUser;
using ExpressMessenger.UsersManagement.Application.Users;
using ExpressMessenger.UsersManagement.Application.Users.GetUser;
using MediatR;

namespace ExpressMessenger.UsersManagement.Api.Users;

internal sealed class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (ISender sender, IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                    .First(claim => claim.Type == "userId")
                    .Value);
                
                GetUserQuery query = new(requestUserId);
                UserDto result = await sender.Send(query);
                GetUserResponse response = new(result.UserId);
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithTags(Tags.Users);
    }
}