using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Authentication;

public interface ITokenGenerator
{
    TokenDto GenerateToken(User user, CancellationToken cancellationToken);
}