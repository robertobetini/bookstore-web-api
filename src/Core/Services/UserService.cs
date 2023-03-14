using Core.Entities;
using Core.Entities.Enums;
using Core.Exceptions;
using Core.Providers;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private const string OWNER_DEFAULT_USERNAME_KEY = Constants.EnvironmentVariableKeys.OWNER_DEFAULT_USERNAME;
    private const string OWNER_DEFAULT_PASSWORD_KEY = Constants.EnvironmentVariableKeys.OWNER_DEFAULT_PASSWORD;

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

        var existingUser = await _userRepository.GetUser(hashedUsername, hashedPassword, cancellationToken);
        if (existingUser is not null)
        {
            return _tokenService.GenerateToken(user.Username, existingUser.AccessLevel);
        }

        return default;
    }

    public async Task CreateAdminUser(User user, CancellationToken cancellationToken = default)
    {
        user.TurnAdminUser();
        await CreateUser(user, cancellationToken);
    }

    public async Task CreateOwnerUser(CancellationToken cancellationToken = default)
    {
        var existingOwner = await _userRepository.GetOwnerUser(cancellationToken);
        if (existingOwner is not null)
        {
            throw new OwnerAlreadyExists();
        }

        var ownerUsername = EnvironmentVariableProvider.GetValue(OWNER_DEFAULT_USERNAME_KEY);
        var ownerPassword = EnvironmentVariableProvider.GetValue(OWNER_DEFAULT_PASSWORD_KEY);
        var user = new User(ownerUsername, ownerPassword, AccessLevel.Owner);
        await CreateUser(user, cancellationToken);
    }

    public async Task CreateRegularUser(User user, CancellationToken cancellationToken = default)
    {
        user.TurnRegularUser();
        await CreateUser(user, cancellationToken);
    }

    public async Task UpdateUserPassword(
        string username, 
        string newPassword, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new UsernameIsNullException();
        }

        var hashedUsername = _hashService.GenerateHash(username);
        var hashedNewPassword = _hashService.GenerateHash(newPassword);

        await _userRepository.UpdateUserPassword(hashedUsername, hashedNewPassword, cancellationToken);
    }

    public async Task UpdateUserAccessLevel(
        string username, 
        AccessLevel accessLevel, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new UsernameIsNullException();
        }

        if (accessLevel is AccessLevel.None)
        {
            throw new InvalidUserAccessLevel();
        }

        if (accessLevel is AccessLevel.Owner)
        {
            throw new OwnerAlreadyExists();
        }

        var hashedUsername = _hashService.GenerateHash(username);
        var userCurrentAccessLevel = await _userRepository.GetUserAccessLevel(username, cancellationToken);
        if (userCurrentAccessLevel == AccessLevel.Owner)
        {
            throw new UpdateOwnerAccessLevelException();
        }

        await _userRepository.UpdateUserAccessLevel(hashedUsername, accessLevel, cancellationToken);
    }

    private async Task CreateUser(User user, CancellationToken cancellationToken = default)
    {
        var hashedUsername = _hashService.GenerateHash(user.Username);
        var hashedPassword = _hashService.GenerateHash(user.Password);

        var existingUser = await _userRepository.GetUser(hashedUsername, hashedPassword, cancellationToken);
        if (existingUser is not null)
        {
            throw new DuplicateUserException();
        }

        await _userRepository.CreateUser(hashedUsername, hashedPassword, user.AccessLevel, cancellationToken);
    }
}
