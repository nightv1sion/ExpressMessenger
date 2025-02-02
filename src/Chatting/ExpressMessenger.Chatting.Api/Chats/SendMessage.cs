using ExpressMessenger.Chatting.Application.Chats.SendMessage;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class SendMessage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("chats/{chatId:guid}/messages", async (
                [FromRoute] Guid chatId,
                [FromBody] SendMessageRequest request,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                SendMessageCommand command = new(
                    chatId,
                    requestUserId,
                    request.Text);
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithTags(Tags.Chats)
            .RequireAuthorization();
    }
}