using System.Linq;
using System.Threading.Tasks;
using FashFuns.Domain.DataContracts.Products;
using FashFuns.Domain.DbConnectionFactory;
using FashFuns.Domain.Entities.Products;
using FashFuns.Domain.Repositories.Products.Contracts;
using FashFuns.Services.ProductServices.Contracts;

namespace FashFuns.Services.ProductServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductsRepositoriesFactory _productRepositoriesFactory;
        private readonly IDbConnectionFactory _connectionFactory;

        public ShoppingCartService(
            IProductsRepositoriesFactory productRepositoriesFactory,
            IDbConnectionFactory connectionFactory)
        {
            _productRepositoriesFactory = productRepositoriesFactory;
            _connectionFactory = connectionFactory;
        }

        public Task<ShoppingCartInfo> GetCart(long userId) =>
            Task.Run(() =>
            {
                using (var connection = _connectionFactory.NewSqlConnection())
                {
                    IShoppingCartRepository repository = _productRepositoriesFactory.NewShoppingCartRepository(connection);

                    ShoppingCart shoppingCart = repository.GetShoppingCartByUser(userId);
                    // test
                    repository.TestDapper();

                    var details = new ShoppingCartInfo();

                    if (shoppingCart != null)
                    {
                        details.OrderItems = shoppingCart.OrderItems;

                        details.Shipping = 30;
                        details.TotalPrice = shoppingCart.OrderItems.Sum(x => (decimal)x.Qty * x.Product.Price);
                        details.OrderTotalPrice = details.TotalPrice + details.Shipping;
                        details.ProductCount = shoppingCart.OrderItems.Count;
                        details.Id = shoppingCart.Id;

                    }


                    return details;
                }
            });
    }
}
