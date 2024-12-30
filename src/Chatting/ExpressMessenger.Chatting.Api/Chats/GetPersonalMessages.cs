using ExpressMessenger.Chatting.Application.Chats.GetPersonalMessages;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetPersonalMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class GetPersonalMessages : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("chats/personal/messages/{companionId:guid}", async (
                [FromRoute] Guid companionId,
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                GetPersonalMessagesQuery command = new(
                    requestUserId,
                    companionId);
                IReadOnlyCollection<PersonalMessageModel> result = await sender.Send(command);
                
                return Results.Ok(
                    new GetPersonalMessagesResponse(result
                        .Select(x => new GetPersonalMessagesResponse.MessageModel(
                            x.Id,
                            x.Text,
                            x.Sent)).ToArray()));
            })
            .WithTags(Tags.Chats);
    }
}