using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateUserDTO
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Password { get; init; }
    [Required]
    public AccessLevel AccessLevel { get; init; }

    public CreateUserDTO(string username, string password, AccessLevel accessLevel)
    {
        Username = username;
        Password = password;
        AccessLevel = accessLevel;
    }
}
