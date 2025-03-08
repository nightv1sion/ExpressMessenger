using ExpressMessenger.Common.Application;

namespace ExpressMessenger.Chatting.Application.Users.ActualizeDeleted;

public sealed record ActualizeDeletedUserCommand(Guid UserId) : ICommand;