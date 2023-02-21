using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateUserDTO
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Password { get; init; }

    public CreateUserDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
