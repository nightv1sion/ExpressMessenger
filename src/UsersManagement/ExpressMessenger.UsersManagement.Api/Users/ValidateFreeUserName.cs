using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.ValidateFreeUserName;
using ExpressMessenger.UsersManagement.Application.Users.ValidateFreeUserName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.UsersManagement.Api.Users;

internal sealed class ValidateFreeUserName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/usernames/validate", async (
                [FromBody] ValidateFreeUserNameRequest request,
                ISender sender) =>
            {
                ValidateFreeUserNameQuery query = new(request.UserName);
                bool result = await sender.Send(query);
                ValidateFreeUserNameResponse response = new(result);
                return Results.Ok(response);
            })
            .WithTags(Tags.Users);
    }
}