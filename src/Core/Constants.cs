namespace Core;

public static class Constants
{
    public static class EnvironmentVariableKeys
    {
        public const string USER_MYSQL_CONNECTION = "UserMySQLConnection";
        public const string BOOKSTORE_MONGODB_CONNECTION = "BookstoreMongoDBConnection";
        public const string JWT_ISSUER = "JwtIssuer";
        public const string JWT_AUDIENCE = "JwtAudience";
        public const string JWT_TOKEN_LIFETIME = "JwtTokenLifetime";
        public const string JWT_SECRET = "JwtSecret";
        public const string OWNER_DEFAULT_USERNAME = "OwnerDefaultUser";
        public const string OWNER_DEFAULT_PASSWORD = "OwnerDefaultPassword";
    }
}
