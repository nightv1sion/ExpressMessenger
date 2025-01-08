using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RegisterUser;
using ExpressMessenger.UsersManagement.Application.Authentication.RegisterUser;
using MediatR;

namespace ExpressMessenger.UsersManagement.Api.Authentication;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/register", async (ISender sender) =>
            {
                RegisterUserCommand command = new();
                RegisteredUserModel result = await sender.Send(command);
                RegisterUserResponse response = new(result.UserId, result.AccessToken, result.RefreshToken);
                return Results.Ok(response);
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }
}