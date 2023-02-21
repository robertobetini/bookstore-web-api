using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services)
    {
        var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer");
        var jwtAudience = Environment.GetEnvironmentVariable("JwtAudience");
        var jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecret");
        var secretBytes = Encoding.UTF8.GetBytes(jwtSecretKey);

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
