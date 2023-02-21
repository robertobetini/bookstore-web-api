using Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions;

public static class AuthenticationExtensions
{
    private const string JWT_ISSUER_KEY = Constants.EnvironmentVariableKeys.JWT_ISSUER;
    private const string JWT_AUDIENCE_KEY = Constants.EnvironmentVariableKeys.JWT_AUDIENCE;
    private const string JWT_SECRET_KEY = Constants.EnvironmentVariableKeys.JWT_SECRET;

    public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services)
    {
        var jwtIssuer = Environment.GetEnvironmentVariable(JWT_ISSUER_KEY);
        var jwtAudience = Environment.GetEnvironmentVariable(JWT_AUDIENCE_KEY);
        var jwtSecretKey = Environment.GetEnvironmentVariable(JWT_SECRET_KEY);
        var secretBytes = Encoding.UTF8.GetBytes(jwtSecretKey!);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
                };
            });

        return services;
    }
}
