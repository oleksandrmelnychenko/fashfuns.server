using Microsoft.EntityFrameworkCore;

namespace FashFuns.Domain.DataSourceAdapters.SQL.Contracts
{
    public interface ISqlDbContext
    {
        DbContext DbContext { get; }
    }
}
