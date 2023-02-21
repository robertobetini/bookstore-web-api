using Core.Entities;

namespace Core.Services.Interfaces;

public interface IUserService
{
    Task Create(User user, CancellationToken cancellationToken = default);
    Task<string?> Authenticate(User user, CancellationToken cancellationToken = default);
}
