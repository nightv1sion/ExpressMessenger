using ExpressMessenger.Common.Domain;

namespace ExpressMessenger.UsersManagement.Domain.UserAggregate;

public sealed class User : AggregateRoot
{
    public DateTimeOffset? RefreshTokenExpired { get; private set; }
    
    public DateTimeOffset Created { get; private init; }
    
    public static User Create()
    {
        return new User
        {
            Id = Guid.CreateVersion7(DateTimeOffset.UtcNow),
            Created = DateTimeOffset.UtcNow,
        };
    }

    public void RefreshToken(DateTimeOffset refreshTokenExpired)
    {
        RefreshTokenExpired = refreshTokenExpired;
    }

    private User() { }
}