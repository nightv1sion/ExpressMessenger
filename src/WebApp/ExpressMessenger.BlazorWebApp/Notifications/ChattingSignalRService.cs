using ExpressMessenger.BlazorWebApp.Apis;
using ExpressMessenger.BlazorWebApp.Authentication;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.Notifications;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace ExpressMessenger.BlazorWebApp.Notifications;

public sealed class ChattingSignalRService(
    ApisSettings apisSettings,
    ITokenManager tokenManager)
{
    private HubConnection? _hubConnection;

    public async Task StartConnectionAsync()
    {
        var (_, accessToken) = await tokenManager.GetAccessToken();
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{apisSettings.Chatting.Url}/notifications/hub?access_token={accessToken}")
            .WithAutomaticReconnect()
            .Build();
        
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }
    }

    public async Task ListenNewChatCreated(
        Func<Task> onNewChatCreated)
    {
        if (_hubConnection == null)
        {
            await StartConnectionAsync();
        }
        
        _hubConnection!.On(NotificationMessages.NewChatCreated, async () =>
        {
            await onNewChatCreated.Invoke();
        });
    }

    public async Task ListenNewMessageReceived(Func<string, Task> onNewMessageReceived)
    {
        if (_hubConnection == null)
        {
            await StartConnectionAsync();
        }

        _hubConnection!.On<string>(NotificationMessages.NewMessageReceived, async chatId =>
        {
            await onNewMessageReceived.Invoke(chatId);
        });
    }
}