using FashFuns.Domain.DataSourceAdapters.SQL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FashFuns.Domain.DataSourceAdapters.SQL
{
    public class SqlDbContext : ISqlDbContext
    {
        public DbContext DbContext { get; }

        public SqlDbContext(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
