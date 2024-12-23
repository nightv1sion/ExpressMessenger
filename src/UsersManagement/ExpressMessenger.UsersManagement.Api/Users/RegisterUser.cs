using ExpressMessenger.Common.Api;
using ExpressMessenger.UsersManagement.Application.Authentication.RegisterUser;
using MediatR;
namespace ExpressMessenger.UsersManagement.Api.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (ISender sender) =>
            {
                RegisterUserCommand command = new();
                RegisteredUserModel result = await sender.Send(command);
                return Results.Ok(result);
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }
}