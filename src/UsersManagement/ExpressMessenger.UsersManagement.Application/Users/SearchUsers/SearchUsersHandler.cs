using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.SearchUsers;

internal sealed class SearchUsersHandler(
    IUserRepository userRepository)
    : IQueryHandler<SearchUsersQuery, IReadOnlyCollection<SearchUserModel>>
{
    public async Task<IReadOnlyCollection<SearchUserModel>> Handle(
        SearchUsersQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<User> users = await userRepository.SearchBy(request.UserNames, cancellationToken);
        
        return users
            .Select(x => new SearchUserModel(
                x.Id,
                x.UserName))
            .ToArray();
    }
}