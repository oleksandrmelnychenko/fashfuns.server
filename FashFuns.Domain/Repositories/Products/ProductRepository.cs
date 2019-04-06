using Dapper;
using FashFuns.Domain.Entities.Products;
using FashFuns.Domain.Repositories.Products.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FashFuns.Domain.Repositories.Products
{
    public class ProductRepository : IProductsRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            _connection.Query<Product, ProductCategory, Product>(
                "SELECT * FROM [Products] " +
                "LEFT JOIN [ProductCategories] ON [ProductCategories].Id = Products.ProductCategoryId",
                (product, category) => {

                    if(category != null)
                    {
                        product.ProductCategory = category;
                    }

                    products.Add(product);

                    return product;
                });

            return products;
        }
    }
}
