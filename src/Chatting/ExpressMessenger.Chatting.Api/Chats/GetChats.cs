using ExpressMessenger.Chatting.Application.Chats.GetChats;
using ExpressMessenger.Common.Api;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChats;
using MediatR;

namespace ExpressMessenger.Chatting.Api.Chats;

internal sealed class GetChats : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("chats", async (
                ISender sender,
                IHttpContextAccessor httpContextAccessor) =>
            {
                Guid requestUserId = Guid.Parse(
                    httpContextAccessor.HttpContext!.User.Claims
                        .First(claim => claim.Type == "userId")
                        .Value);
                
                GetChatsQuery query = new(requestUserId);
                IReadOnlyCollection<ChatModel> result = await sender.Send(query);

                return Results.Ok(
                    new GetChatsResponse(result
                        .Select(chat => new GetChatsResponse.ChatModel(
                            chat.ChatId,
                            chat.Companions
                                .Select(companion =>new GetChatsResponse.ChatModel.CompanionModel(
                                    companion.UserId,
                                    companion.UserName))
                                .ToArray(),
                            chat.Type.ToString()))
                        .ToArray()));
            })
            .RequireAuthorization()
            .WithTags(Tags.Chats);
    }
}