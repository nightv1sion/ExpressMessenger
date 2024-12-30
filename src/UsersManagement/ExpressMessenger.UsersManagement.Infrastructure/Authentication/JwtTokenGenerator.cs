using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpressMessenger.Common.Infrastructure.Authentication;
using ExpressMessenger.UsersManagement.Application.Authentication;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using ExpressMessenger.UsersManagement.Domain.UserAggregate.Exceptions;
using ExpressMessenger.UsersManagement.Infrastructure.Authentication.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpressMessenger.UsersManagement.Infrastructure.Authentication;

internal sealed class JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettingsOptions.Value;
    
    public TokenDto GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
        };

        var accessJwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes),
            signingCredentials: credentials);

        var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        
        user.UpdateRefreshToken(
            null!,
            refreshToken,
            DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationInMinutes));
        
        JwtSecurityTokenHandler tokenHandler = new();
        string? accessToken = tokenHandler.WriteToken(accessJwtSecurityToken);
        
        return new TokenDto(
            accessToken,
            refreshToken);
    }

    public TokenDto RefreshToken(
        User user,
        string accessToken,
        string refreshToken)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
        };

        JwtSecurityTokenHandler tokenHandler = new();

        var oldAccessTokenClaimsPrincipal = tokenHandler.ValidateToken(
            accessToken,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key,
            },
            out _);

        var userIdClaim = oldAccessTokenClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId");

        if (userIdClaim is null || userIdClaim.Value != user.Id.ToString())
        {
            throw new InvalidAccessTokenClaims();
        }

        string newRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        
        user.UpdateRefreshToken(
            refreshToken,
            newRefreshToken,
            DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationInMinutes));

        JwtSecurityToken newAccessJwtSecurityToken = new(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes),
            signingCredentials: credentials);
        
        string? newAccessToken = tokenHandler.WriteToken(newAccessJwtSecurityToken);
        
        return new TokenDto(
            newAccessToken,
            newRefreshToken);
    }
}