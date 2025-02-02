using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUser;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.SearchUsers;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.ValidateFreeUserName;
using Refit;

namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;

public interface IUsersApi
{
    [Get("/users")]
    Task<GetUserResponse> GetUser(
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
    
    [Get("/users/usernames")]
    Task<GetUserNamesResponse> GetUserNames(
        [Query(CollectionFormat.Multi)] IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken = default);

    [Post("/users/usernames/validate")]
    Task<ValidateFreeUserNameResponse> ValidateFreeUserName(
        [Body] ValidateFreeUserNameRequest request,
        CancellationToken cancellationToken = default);
    
    [Post("/users/search")]
    Task<SearchUsersResponse> SearchUsers(
        [Header("Authorization")] string accessToken,
        [Body] SearchUsersRequest request,
        CancellationToken cancellationToken = default);
}