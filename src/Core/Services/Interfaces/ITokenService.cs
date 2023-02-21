using Core.Entities.Enums;

namespace Core.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(string username, AccessLevel userLevel);
    bool ValidateToken(string token);
}
