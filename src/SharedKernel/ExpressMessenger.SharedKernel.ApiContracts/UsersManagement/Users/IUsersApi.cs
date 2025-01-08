using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUser;
using ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;
using Refit;

namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users;

public interface IUsersApi
{
    [Get("/users")]
    Task<GetUserResponse> GetUser(
        [Header("Authorization")] string accessToken,
        CancellationToken cancellationToken = default);
    
    [Get("/users/display-numbers")]
    Task<GetUsersDisplayNumbersResponse> GetUsersDisplayNumbers(
        [Header("Authorization")] string accessToken,
        [Query(CollectionFormat.Multi)] IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken = default);
    
    [Get("/users/display-numbers")]
    Task<GetUsersDisplayNumbersResponse> GetUsersDisplayNumbers(
        [Query(CollectionFormat.Multi)] IReadOnlyCollection<Guid> userIds,
        CancellationToken cancellationToken = default);
}