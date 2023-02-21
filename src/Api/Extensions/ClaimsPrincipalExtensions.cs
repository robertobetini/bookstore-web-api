using Core.Entities.Enums;
using Core.Enums;
using System.Security.Claims;

namespace Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool HasAdminAccess(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaim.UserAccessLevel);

        var accessLevel = Enum.Parse<AccessLevel>(claim?.Value);

        return accessLevel >= AccessLevel.Admin;
    }

    public static bool HasOwnerAccess(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaim.UserAccessLevel);

        var accessLevel = Enum.Parse<AccessLevel>(claim?.Value);

        return accessLevel >= AccessLevel.Owner;
    }

    private static Claim? GetClaim(this ClaimsPrincipal claimsPrincipal, JwtClaim jwtClaim)
    {
        return claimsPrincipal
            .Claims
            .FirstOrDefault(claim =>
                claim.Type == jwtClaim.ToString());
    }
}
