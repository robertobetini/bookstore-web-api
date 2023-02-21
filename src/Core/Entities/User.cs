using Core.Entities.Enums;

namespace Core.Entities;

public class User
{
    public string Username { get; init; }
    public string Password { get; init; }
    public AccessLevel AccessLevel { get; set; }

    public User(string username, string password, AccessLevel accessLevel)
    {
        Username = username;
        Password = password;
        AccessLevel = accessLevel;
    }
}
