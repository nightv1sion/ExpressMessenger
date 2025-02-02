using ExpressMessenger.Chatting.Application.Common;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;

namespace ExpressMessenger.Chatting.Infrastructure.Apis;

internal sealed class UserInfoProvider(
    IUsersApi usersApi) : IUserInfoProvider
{
    public async Task<IReadOnlyDictionary<Guid, string>> GetUserNames(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
    {
        GetUserNamesResponse response = await usersApi.GetUserNames(
            userIds,
            cancellationToken);
        
        return response.UserNames;
    }
}