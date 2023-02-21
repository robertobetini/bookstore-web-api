using System.ComponentModel;

namespace Core.Entities.Enums;

public enum AccessLevel
{
    [Description("None")]
    None,
    [Description("Regular")]
    Regular,
    [Description("Admin")]
    Admin
}
