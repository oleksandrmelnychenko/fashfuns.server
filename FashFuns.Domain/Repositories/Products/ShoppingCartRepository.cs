using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using FashFuns.Domain.DataContracts.Products;
using FashFuns.Domain.Entities.Products;
using FashFuns.Domain.Repositories.Products.Contracts;

namespace FashFuns.Domain.Repositories.Products
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDbConnection _connection;

        public ShoppingCartRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public ShoppingCart GetShoppingCartByUser(long userId)
        {
            ShoppingCart cart = null;

            _connection.Query<ShoppingCart, OrderItem, Product, ShoppingCart>(
                "SELECT * FROM [ShoppingCarts] " +
                "LEFT JOIN [OrderItems] ON OrderItems.ShoppingCartId = ShoppingCarts.Id " +
                "LEFT JOIN [Products] ON OrderItems.ProductId = Products.Id " +
                "WHERE UserIdentityId = @UserId",
                (shoppingCart, orderItem, product) => {

                    if (cart == null)
                    {
                        cart = shoppingCart;
                    }

                    if (orderItem != null)
                    {
                        if (product != null)
                        {
                            orderItem.Product = product;
                        }

                        cart.OrderItems.Add(orderItem);
                    }

                    return shoppingCart;
                },
                new { UserId = userId }
                );

            return cart;
        }
    }
}
