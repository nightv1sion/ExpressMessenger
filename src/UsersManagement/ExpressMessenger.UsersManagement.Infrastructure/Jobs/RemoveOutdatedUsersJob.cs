using ExpressMessenger.UsersManagement.Application.Users.RemoveOutdatedUsers;
using MediatR;
using Quartz;

namespace ExpressMessenger.UsersManagement.Infrastructure.Jobs;

internal sealed class RemoveOutdatedUsersJob(
    ISender sender) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return sender.Send(new RemoveOutdatedUsersCommand());
    }
}