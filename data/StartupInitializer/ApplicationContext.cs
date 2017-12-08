using System;

namespace Titan.Data
{
    public interface IApplicationContext
    {
        string GetConnectionString(string baseConnectionString);
    }
    public class ApplicationContext : IApplicationContext
    {
        private const string DB_HOST_KEY = "POSTGRES_DB_HOST";
        private const string DB_USER_KEY = "POSTGRES_DB_USER";
        private const string DB_PASSWORD_KEY = "POSTGRES_DB_PASSWORD";

        public string GetConnectionString(string baseConnectionString)
        {
            var databaseServer = Environment.GetEnvironmentVariable(DB_HOST_KEY);
            var dbUser = Environment.GetEnvironmentVariable(DB_USER_KEY);
            var dbPwd = Environment.GetEnvironmentVariable(DB_PASSWORD_KEY);
            return string.Format(baseConnectionString, databaseServer, dbUser, dbPwd);
        }
    }
}