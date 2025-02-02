using ExpressMessenger.SharedKernel.ApiContracts.Chatting.CreateChat;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChat;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChats;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.SendMessage;
using Refit;

namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting;

public interface IChatApi
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
    
    [Post("/chats")]
    Task<Guid> CreateChat(
        [Body] CreateChatRequest request,
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
    
    [Post("/chats/{chatId}/messages")]
    Task SendMessage(
        Guid chatId,
        [Body] SendMessageRequest request,
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
}