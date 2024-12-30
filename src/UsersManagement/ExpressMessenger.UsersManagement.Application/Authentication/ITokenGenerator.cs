using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Authentication;

public interface ITokenGenerator
{
    TokenDto GenerateToken(User user);

    TokenDto RefreshToken(
        User user,
        string accessToken,
        string refreshToken);

}