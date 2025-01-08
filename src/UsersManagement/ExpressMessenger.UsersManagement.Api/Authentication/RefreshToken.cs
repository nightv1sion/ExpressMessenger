using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Authentication.RefreshToken;
using ExpressMessenger.UsersManagement.Application.Authentication.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.UsersManagement.Api.Authentication;

internal sealed class RefreshToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/refresh-token", async (
                [FromBody] RefreshTokenRequest request,
                ISender sender) =>
            {
                RefreshTokenCommand command = new(
                    request.UserId,
                    request.AccessToken,
                    request.RefreshToken);
                RefreshTokenModel result = await sender.Send(command);
                RefreshTokenResponse response = new(result.AccessToken, result.RefreshToken);
                return Results.Ok(response);
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
    }
}