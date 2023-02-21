using Core.Entities;
using Core.Entities.Enums;

namespace Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task Create(string username, string password, AccessLevel userAccess, CancellationToken cancellationToken = default);
    Task<User?> Get(string username, string password, CancellationToken cancellationToken = default);
}
