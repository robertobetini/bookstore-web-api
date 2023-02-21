using System.ComponentModel;

namespace Core.Enums;

public enum JwtClaim
{
    [Description("None")]
    None,
    [Description("Username")]
    Username,
    [Description("UserAccessLevel")]
    UserAccessLevel
}
