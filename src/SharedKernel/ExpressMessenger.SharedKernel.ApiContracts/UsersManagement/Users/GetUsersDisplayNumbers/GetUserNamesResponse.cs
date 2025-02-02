namespace ExpressMessenger.SharedKernel.ApiContracts.UsersManagement.Users.GetUsersDisplayNumbers;

public sealed record GetUserNamesResponse(IReadOnlyDictionary<Guid, string> UserNames);