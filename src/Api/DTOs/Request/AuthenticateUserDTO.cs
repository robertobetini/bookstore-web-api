using Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class AuthenticateUserDTO
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Password { get; init; }

    public AuthenticateUserDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
