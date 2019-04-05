
namespace FashFuns.Domain.DataSourceAdapters.SQL.Contracts
{
    public interface ISqlContextFactory
    {
        ISqlDbContext New();
    }
}
