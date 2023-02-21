using Core.Entities.Enums;

namespace Infrastructure.DbEntities.MySQL;

public class UserMySQL
{
    public string Username { get; init; }
    public string Password { get; init; }
    public AccessLevel AccessLevel { get; set; }
    public UserMySQL(string username, AccessLevel accessLevel)
    {
        Username = username;
        AccessLevel = accessLevel;
    }
}
