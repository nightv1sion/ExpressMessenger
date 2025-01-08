using ExpressMessenger.Chatting.Application.Common;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;

namespace ExpressMessenger.Chatting.Infrastructure.Apis;

internal sealed class UserInfoProvider(
    IUsersApi usersApi) : IUserInfoProvider
{
    public async Task<IReadOnlyDictionary<Guid, uint>> GetDisplayNumbers(IReadOnlyCollection<Guid> userIds, CancellationToken cancellationToken)
    {
        GetUsersDisplayNumbersResponse response = await usersApi.GetUsersDisplayNumbers(
            userIds,
            cancellationToken);
        
        return response.DisplayNumbers;
    }
}