using ExpressMessenger.Chatting.Application.Chats.GetChat;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class GetChat : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("chats/{chatId:guid}", async (
                [FromRoute] Guid chatId,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                GetChatQuery query = new(requestUserId, chatId);
                ChatModel chat = await sender.Send(query);

                return Results.Ok(
                    new GetChatResponse(new GetChatResponse.ChatModel(
                        chat.ChatId,
                        chat.Companions
                            .Select(companion => new GetChatResponse.ChatModel.CompanionModel(
                                companion.UserId))
                            .ToArray(),
                        chat.Type.ToString())));
            })
            .RequireAuthorization()
            .WithTags(Tags.Chats);
    }
}