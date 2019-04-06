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

        public decimal ItemTotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal OrderTotal { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
