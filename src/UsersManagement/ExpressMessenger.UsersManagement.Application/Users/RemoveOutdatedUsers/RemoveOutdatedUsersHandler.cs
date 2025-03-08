using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.RemoveOutdatedUsers;

internal sealed class RemoveOutdatedUsersHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveOutdatedUsersCommand>
{
    public async Task Handle(RemoveOutdatedUsersCommand request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<User> users = await userRepository.GetBy(
            DateTimeOffset.UtcNow,
            cancellationToken);
        
        foreach (User user in users)
        {
            userRepository.Remove(user);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}