using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.SearchUsers;
using ExpressMessenger.UsersManagement.Application.Users.SearchUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.UsersManagement.Api.Users;

internal sealed class SearchUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/search", async (
                [FromBody] SearchUsersRequest request,
                ISender sender) =>
            {
                SearchUsersQuery query = new(request.UserNames);
                IReadOnlyCollection<SearchUserModel> result = await sender.Send(query);
                SearchUsersResponse response = new(
                    result
                        .Select(x => new SearchUsersResponse.SearchUserModel(x.UserId, x.UserName))
                        .ToArray());
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithTags(Tags.Users);
    }
}