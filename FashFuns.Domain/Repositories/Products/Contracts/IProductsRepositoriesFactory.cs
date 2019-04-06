using System.Data;

namespace FashFuns.Domain.Repositories.Products.Contracts
{
    public interface IProductsRepositoriesFactory
    {
        IShoppingCartRepository NewShoppingCartRepository(IDbConnection connection);
    }
}
