using System.Data;

namespace FashFuns.Domain.DbConnectionFactory
{
    public interface IDbConnectionFactory
    {
        IDbConnection NewSqlConnection();
    }
}
