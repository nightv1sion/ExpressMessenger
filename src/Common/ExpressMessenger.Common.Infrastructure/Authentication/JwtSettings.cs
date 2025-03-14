namespace ExpressMessenger.Common.Infrastructure.Authentication;

public sealed class JwtSettings
{
    public string Audience { get; init; } = null!;
    
    public string Issuer { get; init; } = null!;
    
    public string Secret { get; init; } = null!;
    
    public int AccessTokenExpirationInMinutes { get; init; }
    
    public int RefreshTokenExpirationInMinutes { get; init; }
}