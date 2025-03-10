using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;
using ExpressMessenger.UsersManagement.Application.Users.GetUsersDisplayNumbers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.UsersManagement.Api.Users;

internal sealed class GetUserNames : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/usernames", async (
                [FromQuery] Guid[] userIds,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                GetUserNamesQuery query = new(userIds);
                IReadOnlyDictionary<Guid, string> result = await sender.Send(query);
                GetUserNamesResponse response = new(result);
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithTags(Tags.Users);
    }
}