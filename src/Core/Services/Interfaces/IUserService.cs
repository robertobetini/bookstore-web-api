using Core.Entities;

namespace Core.Services.Interfaces;

public interface IUserService
{
    Task<string?> Authenticate(User user, CancellationToken cancellationToken = default);
    Task CreateAdminUser(User user, CancellationToken cancellationToken = default);
    Task CreateOwnerUser(CancellationToken cancellationToken = default);
    Task CreateRegularUser(User user, CancellationToken cancellationToken = default);
}
