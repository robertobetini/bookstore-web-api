using Core.Entities.Enums;

namespace Api.DTOs.Request;

public class UpdateUserAccessLevelDTO
{
    public AccessLevel AccessLevel { get; init; }
}
