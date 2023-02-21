using System.Data;

namespace Infrastructure.DbContexts.MySQL.Interfaces;

public interface IUserMySQLContext
{
    public IDbConnection Connection { get; init; }
    public string TableName { get; init; }
}
