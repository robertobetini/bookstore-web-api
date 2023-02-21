using Api.DTOs.Request;
using Core.Entities;
using Core.Entities.Enums;

namespace Api.Adapters;

public static class UserAdapter
{
    public static User ToDomainEntity(CreateUserDTO userDTO)
    {
        return new User(userDTO.Username, userDTO.Password, (AccessLevel)userDTO.AccessLevel);
    }

    public static CreateUserDTO ToUserDTO(User user)
    {
        return new CreateUserDTO(user.Username, user.Password, user.AccessLevel);
    }

    public static User ToDomainEntity(AuthenticateUserDTO userDTO)
    {
        return new User(userDTO.Username, userDTO.Password, default);
    }
}
