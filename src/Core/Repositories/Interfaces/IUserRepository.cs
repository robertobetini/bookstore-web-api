using Core.Entities;
using Core.Entities.Enums;

namespace Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task CreateUser(string username, string password, AccessLevel userAccessLevel, CancellationToken cancellationToken = default);
    Task<User?> GetUser(string username, string password, CancellationToken cancellationToken = default);
    Task<User?> GetOwnerUser(CancellationToken cancellationToken = default);
    Task<AccessLevel?> GetUserAccessLevel(string username, CancellationToken cancellationToken = default);
    Task UpdateUserPassword(string username, string newPassword, CancellationToken cancellationToken = default);
    Task UpdateUserAccessLevel(string username, AccessLevel userAccessLevel, CancellationToken cancellationToken = default);
}
