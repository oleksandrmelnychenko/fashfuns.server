using FashFuns.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace FashFuns.Domain.Repositories.Products.Contracts
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProducts();
    }
}
