using Core.Entities;
using Infrastructure.DbEntities.MySQL;

namespace Infrastructure.Adapters;

public static class UserAdapter
{
    public static User? ToDomainEntity(UserMySQL? userMySQL)
    {
        if (userMySQL is null)
        {
            return default;
        }

        return new(userMySQL.Username, default, userMySQL.AccessLevel);
    }
}
