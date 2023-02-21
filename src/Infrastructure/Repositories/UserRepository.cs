using Core.Entities;
using Core.Entities.Enums;
using Core.Repositories.Interfaces;
using Dapper;
using Infrastructure.Adapters;
using Infrastructure.DbContexts.MySQL.Interfaces;
using Infrastructure.DbEntities.MySQL;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly IDbConnection _connection;
    private readonly string _tableName;

    public UserRepository(IUserMySQLContext mysqlContext)
    {
        _connection = mysqlContext.Connection;
        _tableName = mysqlContext.TableName;
    }

    public async Task<User?> Get(string username, string password, CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"SELECT username, accessLevel FROM users WHERE username = '{username}' AND password = '{password}'";
        var usersMySQL = await _connection.QueryAsync<UserMySQL>(query);

        await CloseConnectionAsync(cancellationToken);

        var userMySQL = usersMySQL.FirstOrDefault();
        return UserAdapter.ToDomainEntity(userMySQL);
    }

    public async Task Create(string username, string password, AccessLevel accessLevel, CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"INSERT INTO {_tableName} VALUES ('{username}', '{password}', '{accessLevel}')";
        _ = await _connection.ExecuteAsync(query);

        await CloseConnectionAsync(cancellationToken);
    }

    public void Dispose()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    private async Task OpenConnectionAsync(CancellationToken cancellationToken = default) => await ((MySqlConnection)_connection).OpenAsync(cancellationToken);
    private async Task CloseConnectionAsync(CancellationToken cancellationToken = default) => await ((MySqlConnection)_connection).CloseAsync(cancellationToken);
}
