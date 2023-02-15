using System.ComponentModel;

namespace Core.Entities.Enums;

public enum BookPreservation
{
    [Description("Undefined")]
    Undefined,
    [Description("New")]
    New,
    [Description("Used")]
    Used,
    [Description("Damaged")]
    Damaged
}
