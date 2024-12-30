using ExpressMessenger.Chatting.Application.Chats.SendPersonalMessage;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.SendPersonalMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class SendPersonalMessage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("chats/personal/messages", async (
                [FromBody] SendPersonalMessageRequest request,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                SendPersonalMessageCommand command = new(
                    request.Text,
                    requestUserId,
                    request.RecipientId);
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithTags(Tags.Chats);
    }
}