using System.Data;
using System.Data.SqlClient;
using FashFuns.Common;

namespace FashFuns.Domain.DbConnectionFactory
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection NewSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.LocalDatabaseConnectionString);
        }
    }
}
