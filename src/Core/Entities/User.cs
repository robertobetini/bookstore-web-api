using Core.Entities.Enums;

namespace Core.Entities;

public class User
{
    public string Username { get; init; }
    public string Password { get; init; }
    public AccessLevel AccessLevel { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        AccessLevel = AccessLevel.None;
    }

    public User(string username, string password, AccessLevel accessLevel)
    {
        Username = username;
        Password = password;
        AccessLevel = accessLevel;
    }

    public void TurnRegularUser()
    {
        AccessLevel = AccessLevel.Regular;
    }

    public void TurnAdminUser()
    {
        AccessLevel = AccessLevel.Admin;
    }
}
