using ExpressMessenger.Chatting.Application.Chats.CreateChat;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.CreateChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class CreateChat : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("chats", async (
                [FromBody] CreateChatRequest request,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);

                CreateChatCommand command = new(requestUserId, request.CompanionIds);
                Guid chatId = await sender.Send(command);
                return Results.Ok(chatId);
            })
            .WithTags(Tags.Chats);
    }
}