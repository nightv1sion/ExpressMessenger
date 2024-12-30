using ExpressMessenger.Common.Domain;
using ExpressMessenger.UsersManagement.Domain.UserAggregate.Exceptions;

namespace ExpressMessenger.UsersManagement.Domain.UserAggregate;

public sealed class User : AggregateRoot
{
    public string? RefreshToken { get; private set; }
    
    public DateTimeOffset? RefreshTokenExpired { get; private set; }
    
    public DateTimeOffset Created { get; private init; }
    
    public static User Create()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Created = DateTimeOffset.UtcNow,
        };
    }

    public void UpdateRefreshToken(
        string oldRefreshToken,
        string refreshToken,
        DateTimeOffset refreshTokenExpired)
    {
        if (!ValidateRefreshToken(oldRefreshToken))
        {
            throw new InvalidRefreshToken();
        }
        
        RefreshToken = refreshToken;
        RefreshTokenExpired = refreshTokenExpired;
    }

    private bool ValidateRefreshToken(string refreshToken)
    {
        return RefreshToken is null && RefreshTokenExpired is null
            || RefreshToken == refreshToken && RefreshTokenExpired > DateTimeOffset.UtcNow;
    }

    private User() { }
}