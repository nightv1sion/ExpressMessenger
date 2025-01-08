using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUsersDisplayNumbers;

internal sealed class GetUsersDisplayNumbersHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUsersDisplayNumbersQuery, IReadOnlyDictionary<Guid, uint>>
{
    public async Task<IReadOnlyDictionary<Guid, uint>> Handle(GetUsersDisplayNumbersQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyDictionary<Guid, uint> displayNumbers = await userRepository.GetDisplayNumbers(
            request.UserIds,
            cancellationToken);
        return displayNumbers;
    }
}