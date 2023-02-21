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

    public async Task<User?> GetUser(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"SELECT username, accessLevel FROM users WHERE username = '{username}' AND password = '{password}'";
        var usersMySQL = await _connection.QueryAsync<UserMySQL>(query);

        await CloseConnectionAsync(cancellationToken);

        var userMySQL = usersMySQL.FirstOrDefault();
        return UserAdapter.ToDomainEntity(userMySQL);
    }

    public async Task CreateUser(
        string username,
        string password,
        AccessLevel userAccessLevel,
        CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"INSERT INTO {_tableName} VALUES ('{username}', '{password}', '{userAccessLevel}')";
        _ = await _connection.ExecuteAsync(query);

        await CloseConnectionAsync(cancellationToken);
    }

    public async Task<User?> GetOwnerUser(CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"SELECT username, accessLevel FROM users WHERE accessLevel = '{AccessLevel.Owner}'";
        var usersMySQL = await _connection.QueryAsync<UserMySQL>(query);

        await CloseConnectionAsync(cancellationToken);

        var userMySQL = usersMySQL.FirstOrDefault();
        return UserAdapter.ToDomainEntity(userMySQL);
    }

    public async Task<AccessLevel?> GetUserAccessLevel(string username, CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"SELECT accessLevel FROM users WHERE username = '{username}'";
        var userAccessLevels = await _connection.QueryAsync<AccessLevel>(query);

        await CloseConnectionAsync(cancellationToken);

        return userAccessLevels.FirstOrDefault();
    }

    public async Task UpdateUserPassword(string username, string newPassword, CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"UPDATE {_tableName} SET password = '{newPassword}' WHERE username = '{username}'";
        _ = await _connection.ExecuteAsync(query);

        await CloseConnectionAsync(cancellationToken);
    }

    public async Task UpdateUserAccessLevel(string username, AccessLevel userAccessLevel, CancellationToken cancellationToken = default)
    {
        await OpenConnectionAsync(cancellationToken);

        var query = $"UPDATE {_tableName} SET accessLevel = '{userAccessLevel}' WHERE username = '{username}'";
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
