using FashFuns.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashFuns.Domain.Repositories.Products.Contracts
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetShoppingCartByUser(long userId);
        void TestDapper();
    }
}
