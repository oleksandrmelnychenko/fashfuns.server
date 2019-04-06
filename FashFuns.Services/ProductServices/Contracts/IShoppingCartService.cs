using FashFuns.Domain.DataContracts.Products;
using System.Threading.Tasks;

namespace FashFuns.Services.ProductServices.Contracts
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartInfo> GetCart(long userId);
    }
}
