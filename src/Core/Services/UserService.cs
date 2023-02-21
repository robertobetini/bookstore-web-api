using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IHashService _hashService;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserService(
        IHashService hashService, 
        ITokenService tokenService, 
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _tokenService = tokenService;
    }

    public async Task<string?> Authenticate(User user, CancellationToken cancellationToken = default)
    {
        var hashedUsername = _hashService.GenerateHash(user.Username);
        var hashedPassword = _hashService.GenerateHash(user.Password);

        var existingUser = await _userRepository.Get(hashedUsername, hashedPassword, cancellationToken);
        if (existingUser is not null)
        {
            return _tokenService.GenerateToken(user.Username, user.AccessLevel);
        }

        return default;
    }

    public async Task Create(User user, CancellationToken cancellationToken = default)
    {
        if (user.AccessLevel is AccessLevel.None)
        {
            throw new InvalidUserAccessLevel();
        }

        var hashedUsername = _hashService.GenerateHash(user.Username);
        var hashedPassword = _hashService.GenerateHash(user.Password);

        var existingUser = await _userRepository.Get(hashedUsername, hashedPassword, cancellationToken);
        if (existingUser is not null)
        {
            throw new DuplicateUserException();
        }

        await _userRepository.Create(hashedUsername, hashedPassword, user.AccessLevel, cancellationToken);
    }
}
