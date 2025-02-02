using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Users.ValidateFreeUserName;

internal sealed class ValidateFreeUserNameHandler(
    IUserRepository userRepository) : IQueryHandler<ValidateFreeUserNameQuery, bool>
{
    public async Task<bool> Handle(ValidateFreeUserNameQuery request, CancellationToken cancellationToken)
    {
        User? userWithUserName = await userRepository.TryGetBy(request.UserName, cancellationToken);
        
        return userWithUserName is null;
    }
}