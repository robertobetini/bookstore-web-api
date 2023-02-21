using Core.Entities;
using Core.Entities.Enums;

namespace Core.Services.Interfaces;

public interface IUserService
{
    Task<string?> Authenticate(User user, CancellationToken cancellationToken = default);
    Task CreateAdminUser(User user, CancellationToken cancellationToken = default);
    Task CreateOwnerUser(CancellationToken cancellationToken = default);
    Task CreateRegularUser(User user, CancellationToken cancellationToken = default);
    Task UpdateUserPassword(string username, string newPassword, CancellationToken cancellationToken = default);
    Task UpdateUserAccessLevel(string username, AccessLevel accessLevel, CancellationToken cancellationToken = default);
}
