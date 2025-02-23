using ExpressMessenger.Chatting.Application.Notifications;
using ExpressMessenger.SharedKernel.ApiContracts.Chatting.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace ExpressMessenger.Chatting.Api.Notifications;

internal sealed class UserNotifier(IHubContext<NotificationHub> notificationHub) : IUserNotifier
{
    public async Task NotifyUsersAboutNewChat(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
    {
        foreach (Guid userId in userIds)
        {
            var group = notificationHub.Clients.Group(userId.ToString());
            await group.SendAsync(
                    NotificationMessages.NewChatCreated,
                    cancellationToken);
        }
    }
    
    public async Task NotifyUsersAboutNewMessage(
        IReadOnlyCollection<Guid> userIds,
        Guid chatId,
        CancellationToken cancellationToken)
    {
        foreach (Guid userId in userIds)
        {
            var group = notificationHub.Clients.Group(userId.ToString());
            await group.SendAsync(
                NotificationMessages.NewMessageReceived,
                chatId,
                cancellationToken);
        }
    }
}