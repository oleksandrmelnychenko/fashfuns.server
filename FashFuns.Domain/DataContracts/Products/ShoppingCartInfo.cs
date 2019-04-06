using FashFuns.Domain.DataContracts.Identity;
using FashFuns.Domain.Entities.Identity;
using FashFuns.Domain.Entities.Products;
using System.Collections.Generic;

namespace FashFuns.Domain.DataContracts.Products
{
    public class ShoppingCartInfo
    {
        public ShoppingCartInfo()
        {
            OrderItems = new List<OrderItem>();
        }

        public long Id { get; set; }

        public int ProductCount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Shipping { get; set; }

        public decimal OrderTotalPrice { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
