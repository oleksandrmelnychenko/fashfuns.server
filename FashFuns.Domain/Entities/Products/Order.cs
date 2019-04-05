using System.Collections.Generic;

namespace FashFuns.Domain.Entities.Products
{
    public class Order : EntityBase
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public OrderStatus OrderStatus { get; set; }

        public string Number { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
