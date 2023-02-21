using Core.Entities.Enums;
using Core.Providers.Interfaces;
using Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class JwtTokenService : ITokenService
{
    private const int DEFAULT_TOKEN_LIFETIME_IN_MINUTES = 120;

    private readonly int _tokenLifetimeInMinutes;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _secret;

    public JwtTokenService(IEnvironmentVariableProvider environmentVariableProvider)
    {
        _issuer = environmentVariableProvider.GetValue("JwtIssuer");
        _audience = environmentVariableProvider.GetValue("JwtAudience");
        _secret = environmentVariableProvider.GetValue("JwtSecret");
        _tokenLifetimeInMinutes = environmentVariableProvider
            .TryGetValue("JwtTokenLifetime", out var tokenLifetimeInMinutes)
            ? DEFAULT_TOKEN_LIFETIME_IN_MINUTES
            : int.Parse(tokenLifetimeInMinutes);
    }

    public string GenerateToken(string username, AccessLevel userLevel)
    {
        var expiration = DateTime.Now.AddMinutes(_tokenLifetimeInMinutes);
        var secretBytes = Encoding.UTF8.GetBytes(_secret);
        var claims = new List<Claim> 
        { 
            new(nameof(username), username),
            new(nameof(userLevel), userLevel.ToString()) 
        };
        var securityKey = new SymmetricSecurityKey(secretBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer, 
            claims: claims,
            audience: _audience, 
            expires: expiration, 
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}
