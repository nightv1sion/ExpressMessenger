using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUsersDisplayNumbers;

internal sealed class GetUserNamesHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserNamesQuery, IReadOnlyDictionary<Guid, string>>
{
    public async Task<IReadOnlyDictionary<Guid, string>> Handle(
        GetUserNamesQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyDictionary<Guid, string> displayNumbers = await userRepository.GetUserNames(
            request.UserIds,
            cancellationToken);
        return displayNumbers;
    }
}