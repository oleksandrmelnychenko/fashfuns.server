using System.Collections.Generic;
using FashFuns.Domain.Entities.Identity;

namespace FashFuns.Domain.Entities.Products
{
    public class ShoppingCart : EntityBase
    {
        public ShoppingCart()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public long UserIdentityId { get; set; }
        public UserIdentity User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
