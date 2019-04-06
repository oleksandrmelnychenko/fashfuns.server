using FashFuns.Domain.DbConnectionFactory;
using FashFuns.Domain.Entities.Products;
using FashFuns.Domain.Repositories.Products.Contracts;
using FashFuns.Services.ProductServices.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FashFuns.Services.ProductServices
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepositoriesFactory _productsRepositoriesFactory;
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductsService(
            IProductsRepositoriesFactory productsRepository,
            IDbConnectionFactory connectionFactory)
        {
            _productsRepositoriesFactory = productsRepository;
            _connectionFactory = connectionFactory;
        }

        public Task<IEnumerable<Product>> GetProducts() =>
            Task.Run(() =>
            {
                using (var connection = _connectionFactory.NewSqlConnection())
                {
                    var repository = _productsRepositoriesFactory.NewProductsRepository(connection);

                    return repository.GetProducts();
                }
            });
        
    }
}
