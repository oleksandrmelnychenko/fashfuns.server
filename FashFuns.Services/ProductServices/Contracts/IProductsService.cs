using FashFuns.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashFuns.Services.ProductServices.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
