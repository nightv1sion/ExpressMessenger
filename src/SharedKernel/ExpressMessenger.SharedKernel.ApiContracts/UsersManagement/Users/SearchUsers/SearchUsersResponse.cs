namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.SearchUsers;

public sealed record SearchUsersResponse(IReadOnlyCollection<SearchUsersResponse.SearchUserModel> Users)
{
    public sealed record SearchUserModel(
        Guid Id,
        string UserName);
};