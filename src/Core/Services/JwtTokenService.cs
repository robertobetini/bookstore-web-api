using Core.Entities.Enums;
using Core.Enums;
using Core.Providers;
using Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class JwtTokenService : ITokenService
{
    private const int DEFAULT_TOKEN_LIFETIME_IN_MINUTES = 120;
    private const string JWT_ISSUER_KEY = Constants.EnvironmentVariableKeys.JWT_ISSUER;
    private const string JWT_AUDIENCE_KEY = Constants.EnvironmentVariableKeys.JWT_AUDIENCE;
    private const string JWT_SECRET_KEY = Constants.EnvironmentVariableKeys.JWT_SECRET;
    private const string JWT_TOKEN_LIFETIME_KEY = Constants.EnvironmentVariableKeys.JWT_TOKEN_LIFETIME;

    private readonly int _tokenLifetimeInMinutes;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _secret;

    public JwtTokenService()
    {
        _issuer = EnvironmentVariableProvider.GetValue(JWT_ISSUER_KEY);
        _audience = EnvironmentVariableProvider.GetValue(JWT_AUDIENCE_KEY);
        _secret = EnvironmentVariableProvider.GetValue(JWT_SECRET_KEY);
        _tokenLifetimeInMinutes = EnvironmentVariableProvider
            .TryGetValue(JWT_TOKEN_LIFETIME_KEY, out var tokenLifetimeInMinutes)
            ? DEFAULT_TOKEN_LIFETIME_IN_MINUTES
            : int.Parse(tokenLifetimeInMinutes);
    }

    public string GenerateToken(string username, AccessLevel userLevel)
    {
        var expiration = DateTime.Now.AddMinutes(_tokenLifetimeInMinutes);
        var secretBytes = Encoding.UTF8.GetBytes(_secret);
        var claims = new List<Claim>
        {
            new(JwtClaim.Username.ToString(), username),
            new(JwtClaim.UserAccessLevel.ToString(), userLevel.ToString())
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
