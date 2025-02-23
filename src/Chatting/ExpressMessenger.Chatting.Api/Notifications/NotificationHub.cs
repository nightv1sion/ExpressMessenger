using Microsoft.AspNetCore.SignalR;

namespace ExpressMessenger.Chatting.Api.Notifications;

public sealed class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        var userId = Context.User!.Claims
            .First(claim => claim.Type == "userId")
            .Value;
        
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
    }
    
    public async Task JoinGroup()
    {
        var userId = Context.User!.Claims
            .First(claim => claim.Type == "userId")
            .Value;
        
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }
}