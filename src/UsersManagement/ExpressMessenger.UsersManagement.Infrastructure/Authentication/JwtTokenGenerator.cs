using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpressMessenger.UsersManagement.Application.Authentication;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpressMessenger.UsersManagement.Infrastructure.Authentication;

internal sealed class JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettingsOptions.Value;
    
    public TokenDto GenerateToken(User user, CancellationToken cancellationToken)
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
        
        user.RefreshToken(DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationInMinutes));
        
        var refreshJwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            expires: user.RefreshTokenExpired!.Value.DateTime,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var accessToken = tokenHandler.WriteToken(accessJwtSecurityToken);
        var refreshToken = tokenHandler.WriteToken(refreshJwtSecurityToken);
        
        return new TokenDto(
            accessToken,
            refreshToken);
    }
}