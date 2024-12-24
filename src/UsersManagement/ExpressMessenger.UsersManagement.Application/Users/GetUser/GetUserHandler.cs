using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.GetUser;

internal sealed class GetUserHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.TryGetById(request.UserId, cancellationToken);
        return new UserDto(user!.Id);
    }
}