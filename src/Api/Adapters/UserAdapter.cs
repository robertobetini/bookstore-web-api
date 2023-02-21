using Api.DTOs.Request;
using Core.Entities;

namespace Api.Adapters;

public static class UserAdapter
{
    public static User ToDomainEntity(CreateUserDTO userDTO) 
    {
        return new User(userDTO.Username, userDTO.Password);
    }

    public static User ToDomainEntity(AuthenticateUserDTO userDTO)
    {
        return new User(userDTO.Username, userDTO.Password);
    }
}
