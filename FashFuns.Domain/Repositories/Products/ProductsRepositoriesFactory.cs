using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FashFuns.Domain.Repositories.Products.Contracts;

namespace FashFuns.Domain.Repositories.Products
{
    public class ProductsRepositoriesFactory : IProductsRepositoriesFactory
    {
        public IShoppingCartRepository NewShoppingCartRepository(IDbConnection connection) =>
            new ShoppingCartRepository(connection);
    }
}
