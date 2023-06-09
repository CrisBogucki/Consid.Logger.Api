namespace BeRemotely.Domain.Configuration;

public static class EnvironmentConfig
{
    public static class Auth
    {
        public static string Secret => Environment.GetEnvironmentVariable("AUTH_SECRET");
    }

    public static class Db
    {
        public static string ConnString => Environment.GetEnvironmentVariable("DB_CONN_STRING");
    }
}