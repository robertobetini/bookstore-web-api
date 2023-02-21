using Infrastructure.DbContexts.MySQL.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.DbContexts.MySQL;

public class UserMySQLContext : IUserMySQLContext
{
    public IDbConnection Connection { get; init; }
    public string TableName { get; init; } = "users";

    public UserMySQLContext(string connectionString)
    {
        Connection = new MySqlConnection(connectionString);
        CreateUserTable();
    }

    private void CreateUserTable()
    {
        Connection.Open();
        var commandText = $"CREATE TABLE IF NOT EXISTS {TableName} (username VARCHAR(100) PRIMARY KEY NOT NULL, password VARCHAR(100) NOT NULL, accessLevel varchar(50) NOT NULL)";
        var command = new MySqlCommand(commandText, Connection as MySqlConnection);
        _ = command.ExecuteNonQuery();
        Connection.Close();
    }
}
