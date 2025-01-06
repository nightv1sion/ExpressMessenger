using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChat;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChats;
using Refit;

namespace ExpressMessenger.BlazorWebApp.Apis.Chatting;

public interface IChattingApi
{
    [Get("/chats")]
    Task<GetChatsResponse> GetChats(
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
    
    [Get("/chats/{chatId}")]
    Task<GetChatResponse> GetChat(
        [Header("Authorization")] string accessToken,
        Guid chatId,
        CancellationToken cancellationToken = default);
}