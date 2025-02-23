namespace ExpressMessenger.Chatting.Application.Notifications;

public interface IUserNotifier
{
    Task NotifyUsersAboutNewChat(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken);

    Task NotifyUsersAboutNewMessage(
        IReadOnlyCollection<Guid> userIds,
        Guid chatId,
        CancellationToken cancellationToken);
}